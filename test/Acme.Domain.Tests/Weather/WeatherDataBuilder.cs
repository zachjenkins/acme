using Acme.Domain.Weather;
using System;

namespace Acme.Domain.Tests.Weather
{
    public class WeatherDataBuilder
    {
        private WeatherDataValue weatherDataValue;

        private WeatherDataBuilder()
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(3);

            // TODO - Add utility code to actually randomize these
            var lowTemp = new TemperatureValue(270.0);
            var temp = new TemperatureValue(274.0);
            var highTemp = new TemperatureValue(275.0);
            var rainChance = 4;
            var cloudCoverPerc = 20;

            weatherDataValue = new WeatherDataValue(startTime,
                                                    endTime,
                                                    temp,
                                                    highTemp,
                                                    lowTemp,
                                                    rainChance,
                                                    cloudCoverPerc);
        }

        public WeatherDataBuilder WithWarmWeather()
        {
            weatherDataValue = new WeatherDataValue(weatherDataValue.StartDate,
                                                    weatherDataValue.EndDate,
                                                    new TemperatureValue(300),
                                                    new TemperatureValue(301),
                                                    new TemperatureValue(299),
                                                    weatherDataValue.RainChance,
                                                    weatherDataValue.CloudCoverPercentage);

            return this;
        }

        public WeatherDataBuilder WithSunnyWeather()
        {
            weatherDataValue = new WeatherDataValue(weatherDataValue.StartDate,
                                                   weatherDataValue.EndDate,
                                                   weatherDataValue.Temperature,
                                                   weatherDataValue.TemperatureHigh,
                                                   weatherDataValue.TemperatureLow,
                                                   0,
                                                   0);

            return this;
        }

        public WeatherDataBuilder WithRainyWeather()
        {
            weatherDataValue = new WeatherDataValue(weatherDataValue.StartDate,
                                                    weatherDataValue.EndDate,
                                                    weatherDataValue.Temperature,
                                                    weatherDataValue.TemperatureHigh,
                                                    weatherDataValue.TemperatureLow,
                                                    50,
                                                    75);


            return this;
        }

        public WeatherDataValue Build() => weatherDataValue;

        public static WeatherDataBuilder Randomized()
            => new WeatherDataBuilder();
    }
}
