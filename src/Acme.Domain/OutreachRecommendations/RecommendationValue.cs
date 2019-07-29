using Acme.Domain.Weather;
using System;

namespace Acme.Domain.OutreachRecommendations
{
    public class RecommendationValue
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public OutreachType RecommendedOutreachCode { get; private set; }
        public string RecommendedOutreachType => RecommendedOutreachCode.ToString();

        public WeatherDataValue Weather { get; private set; }

        public RecommendationValue(DateTime startTime,
                              DateTime endTime,
                              OutreachType outreachType,
                              WeatherDataValue weatherDataValue)
        {
            StartTime = startTime;
            EndTime = endTime;
            RecommendedOutreachCode = outreachType;
            Weather = weatherDataValue ?? throw new ArgumentNullException(nameof(Weather)); ;
        }
    }
}
