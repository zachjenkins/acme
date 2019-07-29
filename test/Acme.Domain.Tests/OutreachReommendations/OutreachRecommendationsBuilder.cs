using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Tests.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.Domain.Tests.OutreachReommendations
{
    public class OutreachRecommendationsBuilder
    {
        private OutreachRecommendation outreachRecommendation;

        private OutreachRecommendationsBuilder()
        {
            var start = DateTime.UtcNow;
            var end = start.AddHours(3);

            // TODO - Use actual random values
            outreachRecommendation = new OutreachRecommendation(
                    "54022",
                    "River Falls",
                    new List<RecommendationValue>
                    {
                        new RecommendationValue(
                            start,
                            end,
                            OutreachType.Email,
                            WeatherDataBuilder.Randomized().Build())
                    }
                );
        }

        public OutreachRecommendation Build() => outreachRecommendation;

        public IEnumerable<OutreachRecommendation> BuildAsList()
            => new List<OutreachRecommendation>
            {
                outreachRecommendation
            };

        public static OutreachRecommendationsBuilder Randomized() => new OutreachRecommendationsBuilder();
    }
}
