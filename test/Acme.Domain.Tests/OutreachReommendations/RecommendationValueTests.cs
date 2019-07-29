using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Tests.Weather;
using System;
using Xunit;

namespace Acme.Domain.Tests.OutreachReommendations
{
    [Trait("Domain", "RecommendationValue")]
    public class RecommendationValueTests
    {
        // Could split this into multiple tests with singular assertions if you fancy that
        [Fact(DisplayName = "Constructor maps input arguments to properties")]
        public void Ctor_Argument_Map()
        {
            // Arrange
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(3);
            var outreachType = OutreachType.Email;
            var weatherDataValue = WeatherDataBuilder.Randomized().Build();

            // Act
            var recommendationValue = new RecommendationValue(startTime,
                                                              endTime,
                                                              outreachType,
                                                              weatherDataValue);

            // Assert
            Assert.Equal(startTime, recommendationValue.StartTime);
            Assert.Equal(endTime, recommendationValue.EndTime);
            Assert.Equal(outreachType, recommendationValue.RecommendedOutreachCode);
            Assert.Equal(weatherDataValue, recommendationValue.Weather);
        }

        [Fact(DisplayName = "Constructor throw ArgumentNullException when WeatherDataValue arg is not provided")]
        public void Ctor_WeatherData_ArgumentException()
        {
            // Arrange
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(3);

            // Act
            var exception = Assert.Throws<ArgumentNullException>(
                () => new RecommendationValue(startTime,
                                              endTime,
                                              OutreachType.Email,
                                              weatherDataValue: null));

            // Assert
            Assert.Contains(nameof(RecommendationValue.Weather), exception.Message);
        }

        [Theory(DisplayName = "RecommendationOutreachType prop displays string value of RecommendationOutreachCode enum")]
        [InlineData(OutreachType.Email, "Email")]
        [InlineData(OutreachType.TextMessage, "TextMessage")]
        [InlineData(OutreachType.PhoneCall, "PhoneCall")]
        [InlineData(OutreachType.DoNotContact, "DoNotContact")]
        public  void RecommendationOutreachType_displays_string(OutreachType type, string expectedValue)
        {
            // Arrange
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(3);
            var weatherDataValue = WeatherDataBuilder.Randomized().Build();

            // Act
            var recommendationValue = new RecommendationValue(startTime,
                                                              endTime,
                                                              type,
                                                              weatherDataValue);

            // Assert
            Assert.Equal(expectedValue, recommendationValue.RecommendedOutreachType);
        }
    }
}
