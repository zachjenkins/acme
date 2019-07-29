using Newtonsoft.Json;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class Wind
    {
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public double Degree { get; set; }
    }
}
