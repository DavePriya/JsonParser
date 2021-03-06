using Newtonsoft.Json;

namespace JsonParser.Services.Models
{
    public class LineItem2Model
    {
        [JsonProperty(PropertyName = "Container")]
        public string Container { get; set; }

        [JsonProperty(PropertyName = "ShippingLineSeal")]
        public string ShippingLineSeal { get; set; }

        [JsonProperty(PropertyName = "Containersize")]
        public string ContainerSize { get; set; }

        [JsonProperty(PropertyName = "Gr_Weight")]
        public string Gr_Weight { get; set; }

        [JsonProperty(PropertyName = "GR_Units")]
        public string GR_Units { get; set; }

        [JsonProperty(PropertyName = "Volume")]
        public string Volume { get; set; }

        [JsonProperty(PropertyName = "Volume_Units")]
        public string Volume_Units { get; set; }

        [JsonProperty(PropertyName = "Packages")]
        public string Packages { get; set; }

        [JsonProperty(PropertyName = "Packages_Units")]
        public string Packages_Units { get; set; }

        [JsonProperty(PropertyName = "HS_CODE")]
        public string HSCode { get; set; }
    }
}
