using Acme.Domain.Weather;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Acme.Domain.OutreachRecommendations
{
    public class OutreachRecommendation
    {
        private IList<RecommendationValue> recommendations = new List<RecommendationValue>();

       // public string Id { get; private set; }
        public string ZipCode { get; private set; }
        public string CityName { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public IEnumerable<RecommendationValue> Recommendations
        {
            get => recommendations;
            private set => recommendations = value?.ToList();
        }

        public OutreachRecommendation(string zipCode,
                                      string cityName,
                                      IEnumerable<RecommendationValue> recommendations)
        {
            ZipCode = zipCode;
            CityName = cityName;
            this.recommendations = recommendations.ToList();

            LastUpdated = DateTime.UtcNow;
        }
    }
}
