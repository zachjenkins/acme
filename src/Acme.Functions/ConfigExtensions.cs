using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Registration;
using Acme.Functions;
using Acme.Infrastructure.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Net.Http;
using RestEase;
using Acme.Plugins.OpenWeatherMap.Internal;
using Acme.Domain.Weather;
using Acme.Plugins.OpenWeatherMap;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Acme.Functions
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            MongoMaps.RegisterMaps();

            var mongoConnectionString = Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING");

            var client = new MongoClient(mongoConnectionString);
            var database = client.GetDatabase("acme");

            services.AddSingleton<IMongoCollection<OutreachRecommendation>>(s =>
            {
                var outreachRecCollection = database.GetCollection<OutreachRecommendation>("outreachRecommendations");
                return outreachRecCollection;
            });

            services.AddSingleton<IMongoCollection<ZipRegistration>>(s =>
            {
                var registrationCollection = database.GetCollection<ZipRegistration>("registrations");
                return registrationCollection;
            });

            return services;
        }

        public static IServiceCollection AddServiceBusPublisher(this IServiceCollection services)
        {
            var connStringBuilder = new ServiceBusConnectionStringBuilder(endpoint: Environment.GetEnvironmentVariable("SB_ENDPOINT"),
                                                                          entityPath: Environment.GetEnvironmentVariable("SB_ENTITY"),
                                                                          sharedAccessKeyName: Environment.GetEnvironmentVariable("SB_ACCESS_KEY_NAME"),
                                                                          sharedAccessKey: Environment.GetEnvironmentVariable("SB_ACCESS_KEY"));

            services.AddSingleton<IQueueClient>(s =>
            {
                return new QueueClient(connStringBuilder);
            });
            
            return services;
        }

        public static IServiceCollection AddWeatherChannelService(this IServiceCollection services)
        {
            var host = Environment.GetEnvironmentVariable("WEATHER_CHANNEL_HOST");
            var version = Environment.GetEnvironmentVariable("WEATHER_CHANNEL_VERSION");
            var apiKey = Environment.GetEnvironmentVariable("WEATHER_CHANNEL_APIKEY");
            services.AddScoped<IWeatherForecastService, OpenWeatherMapForecastService>();

            services.AddSingleton<IOpenWeatherMapApi>((s) =>
            {

                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(host),
                    Timeout = TimeSpan.FromSeconds(10)
                };

                var weatherApi = new RestClient(httpClient).For<IOpenWeatherMapApi>();
                weatherApi.Version = version;
                weatherApi.ApiKey = apiKey;

                return weatherApi;
            });


            return services;
        }
    }
}
