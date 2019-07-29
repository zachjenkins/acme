using Acme.Plugins.OpenWeatherMap.Internal.Models;
using RestEase;
using System.Threading.Tasks;

namespace Acme.Plugins.OpenWeatherMap.Internal
{
    [Header("Cache-Control", "no-cache")]
    public interface IOpenWeatherMapApi
    {
        [Query(Name = "APPID")]
        string ApiKey { get; set; }

        [Path("version")]
        string Version { get; set; }

        [Get("data/{version}/forecast")]
        Task<OpenWeatherResponse> GetWeatherDataByZip([Query(Name = "zip")] string zipCode);
    }
}
