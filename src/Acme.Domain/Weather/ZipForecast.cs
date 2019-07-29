using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Acme.Domain.Weather
{
    public class ZipForecast
    {
        private IList<WeatherDataValue> weatherData = new List<WeatherDataValue>();

        public string ZipCode { get; }
        public string CityName { get; }
        public IEnumerable<WeatherDataValue> WeatherData => weatherData.OrderBy(d => d.StartDate);

        public ZipForecast(string zipCode,
                           string cityName,
                           IEnumerable<WeatherDataValue> weatherData)
        {
            if (weatherData == null | !weatherData.Any())
                throw new ArgumentException("Must provide at least one weather data");

            ZipCode = zipCode;
            CityName = cityName;

            this.weatherData = weatherData.ToList();
        }
    }
}
