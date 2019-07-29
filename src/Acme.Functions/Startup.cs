using Acme.Domain;
using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Registration;
using Acme.Functions;
using Acme.Infrastructure.EventPublisher;
using Acme.Infrastructure.Repositories;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Acme.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddMongo()
                .AddServiceBusPublisher()
                .AddWeatherChannelService()
                .AddScoped<IOutreachRecommendationRepository, MongoOutreachRecommendationsRepository>()
                .AddScoped<IRegistrationRepository, MongoRegistrationRepository>()
                .AddScoped<IEventPublisher, ServiceBusQueueEventPublisher>()
                .AddScoped<IRecommendationFactory, RecommendationFactory>()
                .AddMediatR(typeof(Startup).Assembly);
        }
    }
}
