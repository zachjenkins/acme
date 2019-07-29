using Newtonsoft.Json;
using System.Collections.Generic;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class OpenWeatherResponse
    {
        [JsonProperty("cod")]
        public string Code { get; set; }
        public double Message { get; set; }
        [JsonProperty("cnt")]
        public int Count { get; set; }
        public IEnumerable<ListItem> List { get; set; }
        public City City { get; set; }
    }
}
