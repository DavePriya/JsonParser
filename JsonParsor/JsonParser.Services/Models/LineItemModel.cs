using Newtonsoft.Json;

namespace JsonParser.Services.Models
{
    public class LineItemModel
    {
        [JsonProperty(PropertyName = "SrNo")]
        public string SrNo { get; set; }

        [JsonProperty(PropertyName = "MarksandNumbers")]
        public string MarksNumbers { get; set; }

        [JsonProperty(PropertyName = "Pkg_Qty")]
        public string NumberPackages { get; set; }

        [JsonProperty(PropertyName = "Pkg_Type")]
        public string Pkg_Type{ get; set; }

        [JsonProperty(PropertyName = "DescriptionofGoods")]
        public string DescriptionGoods { get; set; }

        [JsonProperty(PropertyName = "Gr_Weight")]
        public string GrossWeight { get; set; }

        [JsonProperty(PropertyName = "Gr_Units")]
        public string Gr_Units { get; set; }

        [JsonProperty(PropertyName = "Measurement_Volume")]
        public string Measurement { get; set; }

        [JsonProperty(PropertyName = "Measurement_Units")]
        public string Measurement_Units { get; set; }
    }
}
