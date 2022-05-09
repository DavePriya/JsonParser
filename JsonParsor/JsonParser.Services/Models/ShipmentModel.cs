using Newtonsoft.Json;

namespace JsonParser.Services.Models
{
    public class ShipmentModel
    {
        [JsonProperty(PropertyName = "DeliveryAgentContact")]
        public string DeliveryAgentContact { get; set; }

        [JsonProperty(PropertyName = "Vesselname")]
        public string VesselName { get; set; }

        [JsonProperty(PropertyName = "ConsignorAddress")]
        public string ConsignorAddress { get; set; }

        [JsonProperty(PropertyName = "PlaceofDischarge")]
        public string PlaceOfDischarge { get; set; }

        [JsonProperty(PropertyName = "ShippingBillDate")]
        public string ShippingBillDate { get; set; }
        
        [JsonProperty(PropertyName = "PortofLoading")]
        public string PortOfLoading { get; set; }

        [JsonProperty(PropertyName = "MoveType")]
        public string MoveType { get; set; }

        [JsonProperty(PropertyName = "NotifyParty1Address")]
        public string NotifyParty1Address { get; set; }

        [JsonProperty(PropertyName = "MTD/BLNo")]
        public string MTDBLNo { get; set; }

        [JsonProperty(PropertyName = "DocumentType")]
        public string DocumentType { get; set; }

        public string Consignor { get; set; }

        public string Consignee { get; set; }

        [JsonProperty(PropertyName = "P_O")]
        public string PONo { get; set; }

        [JsonProperty(PropertyName = "BLType(OBL/SWB)")]
        public string ReleaseType { get; set; }

        [JsonProperty(PropertyName = "DateofIssue")]
        public string DateofIssue { get; set; }

        [JsonProperty(PropertyName = "ShippingBill")]
        public string ShippingBill { get; set; }

        [JsonProperty(PropertyName = "DeliveryAgentContactAddress")]
        public string DeliveryAgentContactAddress { get; set; }

        [JsonProperty(PropertyName = "ShipmentReferenceNo")]
        public string ShipmentReferenceNo { get; set; }

        [JsonProperty(PropertyName = "PlaceofAcceptance")]
        public string PlaceOfAcceptance { get; set; }

        [JsonProperty(PropertyName = "NotifyParty1")]
        public string NotifyParty1 { get; set; }

        [JsonProperty(PropertyName = "NotifyParty2")]
        public string NotifyParty2 { get; set; }

        [JsonProperty(PropertyName = "PaymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonProperty(PropertyName = "ConsigneeAddress")]
        public string ConsigneeAddress { get; set; }

        public string Packages { get; set; }

        //[JsonProperty(PropertyName = "HS Code")]
        //public string HSCode { get; set; }

        [JsonProperty(PropertyName = "VoyageNo")]
        public string VoyageNo { get; set; }

        [JsonProperty(PropertyName = "NotifyParty2Address")]
        public string NotifyParty2Address { get; set; }

        [JsonProperty(PropertyName = "PlaceofDelivery")]
        public string PlaceOfDelivery { get; set; }

        // public LineItemDataModel LineItems { get; set; }
        public LineItemDataModel LineItem1 { get; set; }

        public LineItem2DataModel LineItem2 { get; set; }

    }

}
