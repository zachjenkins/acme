using Acme.Domain.Weather;

namespace Acme.Domain.OutreachRecommendations
{
    public interface IRecommendationFactory
    {
        RecommendationValue Create(WeatherDataValue weatherDataValue);
    }
}
