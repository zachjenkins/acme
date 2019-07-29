using Newtonsoft.Json;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Timezone { get; set; }
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
    }
}
