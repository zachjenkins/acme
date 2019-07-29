using System.Threading.Tasks;

namespace Acme.Domain.Weather
{
    public interface IWeatherForecastService
    {
        Task<ZipForecast> GetForecastForZip(string zipCode);
    }
}
