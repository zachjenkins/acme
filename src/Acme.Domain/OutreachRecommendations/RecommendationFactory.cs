using Acme.Domain.Weather;
using System;

namespace Acme.Domain.OutreachRecommendations
{
    public class RecommendationFactory : IRecommendationFactory
    {
        public RecommendationValue Create(WeatherDataValue weatherDataValue)
        {
            if (weatherDataValue == null)
                throw new ArgumentNullException(nameof(weatherDataValue));

            switch (weatherDataValue)
            {
                case WeatherDataValue w when w.IsSunny && w.IsWarm && !w.IsRaining:
                    return Build(w, OutreachType.TextMessage);
                case WeatherDataValue w when w.IsMild:
                    return Build(w, OutreachType.Email);
                case WeatherDataValue w when w.IsRaining || w.IsCold:
                    return Build(w, OutreachType.PhoneCall);
                default:
                    return Build(weatherDataValue, OutreachType.DoNotContact);
            }
        }

        private static RecommendationValue Build(WeatherDataValue weatherData, OutreachType outreachType)
            => new RecommendationValue(weatherData.StartDate, weatherData.EndDate, outreachType, weatherData);
    }
}
