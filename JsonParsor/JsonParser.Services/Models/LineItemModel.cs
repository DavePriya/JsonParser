using Newtonsoft.Json;

namespace JsonParser.Services.Models
{
    public class LineItemModel
    {
        [JsonProperty(PropertyName = "Sr No")]
        public string SrNo { get; set; }

        [JsonProperty(PropertyName = "Marks and Number")]
        public string MarksNumbers { get; set; }

        [JsonProperty(PropertyName = "Number of  Packages")]
        public string NumberPackages { get; set; }

        [JsonProperty(PropertyName = "Description of Goods")]
        public string DescriptionGoods { get; set; }

        [JsonProperty(PropertyName = "Gross  Weight")]
        public string GrossWeight { get; set; }

        [JsonProperty(PropertyName = "Measurement / CBM")]
        public string Measurement { get; set; }


    }
}
