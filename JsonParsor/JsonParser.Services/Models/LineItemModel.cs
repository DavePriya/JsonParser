using Newtonsoft.Json;

namespace JsonParser.Services.Models
{
    public class LineItemModel
    {
        [JsonProperty(PropertyName = "SrNo")]
        public string SrNo { get; set; }

        [JsonProperty(PropertyName = "MarksandNumbers")]
        public string MarksNumbers { get; set; }

        [JsonProperty(PropertyName = "NumberofPackages")]
        public string NumberPackages { get; set; }

        [JsonProperty(PropertyName = "DescriptionofGoods")]
        public string DescriptionGoods { get; set; }

        [JsonProperty(PropertyName = "GrossWeight")]
        public string GrossWeight { get; set; }

        [JsonProperty(PropertyName = "Measurement/CBM")]
        public string Measurement { get; set; }


    }
}
