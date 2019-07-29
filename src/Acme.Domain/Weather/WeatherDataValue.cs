using Newtonsoft.Json;
using System;

namespace Acme.Domain.Weather
{
    public class WeatherDataValue
    {
        [JsonIgnore] public DateTime StartDate { get; private set; }
        [JsonIgnore] public DateTime EndDate { get; private set; }
        public TemperatureValue Temperature { get; private set; }
        public TemperatureValue TemperatureHigh { get; private set; }
        public TemperatureValue TemperatureLow { get; private set; }
        public int CloudCoverPercentage { get; private set; }
        public double RainChance { get; private set; }
        [JsonIgnore] public bool IsSunny => CloudCoverPercentage < 20;
        [JsonIgnore] public bool IsRaining => RainChance > 20;
        [JsonIgnore] public bool IsWarm => TemperatureHigh.Fahrenheit > 75;
        [JsonIgnore] public bool IsMild => TemperatureHigh.Fahrenheit < 75 && TemperatureHigh.Fahrenheit > 55;
        [JsonIgnore] public bool IsCold => TemperatureHigh.Fahrenheit < 55;

        public WeatherDataValue(DateTime startDate,
                                DateTime endDate,
                                TemperatureValue temperature,
                                TemperatureValue highTemperature,
                                TemperatureValue lowTemperature,
                                double rainChance,
                                int cloudPercentage)
        {
            StartDate = startDate;
            EndDate = endDate;
            Temperature = temperature;
            TemperatureHigh = highTemperature;
            TemperatureLow = lowTemperature;
            RainChance = rainChance;
            CloudCoverPercentage = cloudPercentage;
        }
    }
}
