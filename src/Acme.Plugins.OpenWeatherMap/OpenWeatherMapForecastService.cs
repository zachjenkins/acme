using Acme.Domain.Weather;
using Acme.Plugins.OpenWeatherMap.Internal;
using Acme.Plugins.OpenWeatherMap.Internal.Models;
using Newtonsoft.Json;
using RestEase;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Plugins.OpenWeatherMap
{
    public class OpenWeatherMapForecastService : IWeatherForecastService
    {
        private readonly IOpenWeatherMapApi openWeatherMapApi;

        public OpenWeatherMapForecastService(IOpenWeatherMapApi openWeatherMapApi)
        {
            this.openWeatherMapApi = openWeatherMapApi
                ?? throw new ArgumentNullException(nameof(openWeatherMapApi));
        }

        public async Task<ZipForecast> GetForecastForZip(string zipCode)
        {
            try
            {
                var openWeatherMapData = await openWeatherMapApi.GetWeatherDataByZip(zipCode);

                return Map(zipCode, openWeatherMapData);
            }
            catch (ApiException apiException)
            {
                // Rethrow Custom Exception or retry
                throw apiException;
            }
        }

        private ZipForecast Map(string zipCode, OpenWeatherResponse openWeatherResponse)
        {   
            var weatherDataValues = openWeatherResponse.List.Select(listItem =>
            {
                
                var temp = new TemperatureValue(listItem.Main.Temp);
                var highTemp = new TemperatureValue(listItem.Main.TempMax);
                var lowTemp = new TemperatureValue(listItem.Main.TempMin);

                return new WeatherDataValue(startDate: listItem.StartDate,
                                            endDate: listItem.StartDate.AddHours(3),
                                            temperature: temp,
                                            highTemperature: highTemp,
                                            lowTemperature: lowTemp,
                                            rainChance: listItem.Rain?.Chance ?? 0,
                                            cloudPercentage: listItem.Clouds.All
                    );
            }).ToList();
            
            return new ZipForecast(zipCode, openWeatherResponse.City.Name, weatherDataValues);
        }
    }
}
