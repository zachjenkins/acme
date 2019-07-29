using Newtonsoft.Json;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class Main
    {
        public double Temp { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public double Humidity { get; set; }
    }
}
