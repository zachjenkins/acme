using Acme.Domain;
using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Weather;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Acme.Functions.Commands
{
    public class UpdateZipForecast
    {
        public class Command : IRequest
        {
            public WeatherUpdateTriggerEvent WeatherUpdateEvent { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRecommendationFactory recommendationFactory;
            private readonly IWeatherForecastService weatherForecastService;
            private readonly IOutreachRecommendationRepository outreachRecommendationRepository;

            public Handler(IRecommendationFactory recommendationFactory, 
                           IOutreachRecommendationRepository outreachRecommendationRepository, 
                           IWeatherForecastService weatherForecastService)
            {
                this.recommendationFactory = recommendationFactory;
                this.outreachRecommendationRepository = outreachRecommendationRepository;
                this.weatherForecastService = weatherForecastService;
            }
         
            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var weatherUpdateEvent = command.WeatherUpdateEvent;

                var weatherForcastForZip = await weatherForecastService.GetForecastForZip(weatherUpdateEvent.ZipCode);
                
                var recommendations = weatherForcastForZip.WeatherData.Select(d => recommendationFactory.Create(d)).ToList();

                var outreachRecommendation = new OutreachRecommendation(weatherForcastForZip.ZipCode,
                                                                        weatherForcastForZip.CityName,
                                                                        recommendations);

                await outreachRecommendationRepository.Update(outreachRecommendation);

                return Unit.Value;
            }
        }
    }
}
