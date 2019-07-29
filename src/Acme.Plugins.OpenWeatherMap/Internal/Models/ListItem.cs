using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Acme.Plugins.OpenWeatherMap.Internal.Models
{
    public class ListItem
    {
        [JsonProperty("dt")]
        public int DateStartEpoch { get; set; }
        [JsonProperty("dt_txt")]
        public DateTime StartDate { get; set; }
        public Main Main { get; set; }
        public IEnumerable<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }

    }
}
