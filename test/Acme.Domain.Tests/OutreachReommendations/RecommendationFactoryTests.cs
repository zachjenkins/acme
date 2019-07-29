using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Tests.Weather;
using System;
using Xunit;

namespace Acme.Domain.Tests.OutreachReommendations
{
    [Trait("Domain", "RecommendationFactory")]
    public class RecommendationFactoryTests
    {
        private readonly RecommendationFactory recommendationFactory = new RecommendationFactory();

        [Fact(DisplayName = "Create produces text message recommendation when it is sunny, warm, and not raining")]
        public void Create_TextMessage()
        {
            // Arrange
            var weatherData = WeatherDataBuilder.Randomized()
                .WithWarmWeather()
                .WithSunnyWeather()
                .Build();

            // Act
            var recommendation = recommendationFactory.Create(weatherData);

            // Assert
            Assert.Equal(OutreachType.TextMessage, recommendation.RecommendedOutreachCode);
        }

        [Fact(DisplayName = "Create throws ArgumentNullException when WeatherDataValue input is null")]
        public void Create_Throws_ArgumentNullException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(()
                => recommendationFactory.Create(null));
        }
    }
}
