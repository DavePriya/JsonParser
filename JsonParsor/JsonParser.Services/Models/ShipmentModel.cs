using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonParser.Services.Models
{
    public class ShipmentModel
    {
        [JsonProperty(PropertyName = "Delivery Agent Contact")]
        public string DeliveryAgentContact { get; set; }

        [JsonProperty(PropertyName = "Vessel Name")]
        public string VesselName { get; set; }

        [JsonProperty(PropertyName = "Shipping Line Seal")]
        public string ShippingLineSeal { get; set; }

        [JsonProperty(PropertyName = "Container Size")]
        public string ContainerSize { get; set; }

        [JsonProperty(PropertyName = "Consignor Address")]
        public string ConsignorAddress { get; set; }

        [JsonProperty(PropertyName = "Place of Discharge")]
        public string PlaceOfDischarge { get; set; }

        [JsonProperty(PropertyName = "Shipping Bill Date")]
        public string ShippingBillDate { get; set; }
        
        [JsonProperty(PropertyName = "Port Of Loading")]
        public string PortOfLoading { get; set; }

        [JsonProperty(PropertyName = "Move Type")]
        public string MoveType { get; set; }

        [JsonProperty(PropertyName = "Volume")]
        public string Volume { get; set; }

        [JsonProperty(PropertyName = "Notify Party 1 Address")]
        public string NotifyParty1Address { get; set; }

        [JsonProperty(PropertyName = "MTD / BL No")]
        public string MTDBLNo { get; set; }

        [JsonProperty(PropertyName = "Document Type")]
        public string DocumentType { get; set; }

        public string Consignor { get; set; }

        public string Consignee { get; set; }

        [JsonProperty(PropertyName = "PO No")]
        public string PONo { get; set; }

        [JsonProperty(PropertyName = "Container No")]
        public string ContainerNo { get; set; }

        public string Weight { get; set; }

        [JsonProperty(PropertyName = "Shipping Bill")]
        public string ShippingBill { get; set; }

        [JsonProperty(PropertyName = "Delivery Agent Contact Address")]
        public string DeliveryAgentContactAddress { get; set; }

        [JsonProperty(PropertyName = "Shipment Reference No")]
        public string ShipmentReferenceNo { get; set; }

        [JsonProperty(PropertyName = "Place of Acceptance")]
        public string PlaceOfAcceptance { get; set; }

        [JsonProperty(PropertyName = "Notify Party 1")]
        public string NotifyParty1 { get; set; }

        [JsonProperty(PropertyName = "Notify Party 2")]
        public string NotifyParty2 { get; set; }

        [JsonProperty(PropertyName = "Payment Term")]
        public string PaymentTerm { get; set; }

        [JsonProperty(PropertyName = "Consignee Address")]
        public string ConsigneeAddress { get; set; }

        public string Packages { get; set; }

        [JsonProperty(PropertyName = "HS Code")]
        public string HSCode { get; set; }

        [JsonProperty(PropertyName = "Voyage No")]
        public string VoyageNo { get; set; }

        [JsonProperty(PropertyName = "Notify Party 2 Address")]
        public string NotifyParty2Address { get; set; }

        [JsonProperty(PropertyName = "Place of Delivery")]
        public string PlaceOfDelivery { get; set; }

        public LineItemDataModel LineItems { get; set; }
    }

}
