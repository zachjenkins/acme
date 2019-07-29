using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using Acme.Functions.Queries;
using Acme.Functions.Commands;
using Acme.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace Acme.Functions
{
    public class Functions
    {
        private readonly IQueueClient queueClient;
        private readonly IMediator mediator;
        private readonly IEventPublisher eventPublisher;

        public Functions(IMediator mediator, IEventPublisher eventPublisher, IQueueClient queueClient)
        {
            this.queueClient = queueClient;
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.eventPublisher = eventPublisher;
        }

        [FunctionName("GetOutreachRecommendationsFunc")]
        public async Task<IActionResult> GetRecommendations(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "outreachRecommendations")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"GetOutreachRecommendationsFunc triggered at {DateTime.UtcNow}");

            var zipCodes = req.Query["zipCodes"] as IEnumerable<string>;
            var cities = req.Query["cities"] as IEnumerable<string>;

            var query = new GetOutreachRecommendations.Query
            {
                Cities = cities,
                ZipCodes = zipCodes
            };

            // TODO - Define custom exceptions or result messages; Error model
            try
            {
                var result = await mediator.Send(query);

                return new OkObjectResult(result);
            }
            catch (Exception exception)
            {
                log.LogError($"Exception: {exception.StackTrace}");
                return new BadRequestObjectResult(exception.Message);
            }
        }

        [FunctionName("GetRegisteredZips")]
        public async Task<IActionResult> GetRegisteredZips(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "registrations")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"GetRegisteredZips triggered at {DateTime.UtcNow}");

            var query = new GetRegistrations.Query();

            var result = await mediator.Send(query);

            return new OkObjectResult(result);
        }

        [FunctionName("WeatherDataCollectionTrigger")]
        //public async Task TriggerCollection([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        public async Task TriggerCollection([HttpTrigger(AuthorizationLevel.Function, "post", Route = "trigger")] HttpRequest req, ILogger log)
        {
            log.LogInformation($"WeatherDataCollectionTrigger triggered at {DateTime.UtcNow}");

            var zip = req.Query["Zip"].ToList().FirstOrDefault();

            var query = new GetRegistrations.Query();

            var registrations = await mediator.Send(query);

            var weatherEvents = registrations.Select(r => new WeatherUpdateTriggerEvent(r.ZipCode));

            await eventPublisher.PublishEvents(weatherEvents);

        }

        [FunctionName("WeatherCollectionRequestedHandler")]
        public async Task HandleWeatherUpdate([ServiceBusTrigger("weatherDataEventQueue",  Connection = "SERVICE_BUS_CONNECTION")] Message message, ILogger log)
        {
            try
            {
                var stringValue = Encoding.UTF8.GetString(message.Body, 0, message.Body.Length);

                var weatherUpdateEvent = JsonConvert.DeserializeObject<WeatherUpdateTriggerEvent>(stringValue);

                log.LogInformation($"WeatherCollectionRequestedHandler for Zip {weatherUpdateEvent.ZipCode} triggered at {DateTime.UtcNow}");

                var command = new UpdateZipForecast.Command
                {
                    WeatherUpdateEvent = weatherUpdateEvent
                };

                await mediator.Send(command);
            }
            catch (JsonException jsonException)
            {
                await queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                log.LogError($"Error occured when parsing message.");
            }
            catch (Exception e)
            {
                await queueClient.AbandonAsync(message.SystemProperties.LockToken);
                log.LogError(e.StackTrace);
            }

            
        }
    }
}
