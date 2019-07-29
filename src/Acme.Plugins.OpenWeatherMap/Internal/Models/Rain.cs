using Newtonsoft.Json;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double? Chance { get; set; }
    }
}
