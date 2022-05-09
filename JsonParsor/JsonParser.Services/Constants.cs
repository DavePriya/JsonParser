using System.Collections.Generic;

namespace JsonParser.Services
{
    public static class Constants
    {
        public const string EnterpriseID = "TIP";
        public const string ServerID = "TS3";
        public const string WeightUnitCode = "KG";
        public const string WeightUnitDescription = "Kilograms";
        public const string VolumeUnitCode = "M3";
        public const string VolumeUnitDescription = "Cubic Metres";
        public const string PackageUnitCode = "PKG";
        public const string PackageUnitDescription = "Package";
        public const string IncoTerm = "FOB";
        public const string BookingStatus = "Confirmed";
        public const string LegType = "Main";
        public const string TransportMode = "Sea";
        public const string BillIssued = "BillIssued";
        public const string WayBillTypeCode = "HWB";
        public const string WayBillTypeDesc = "House Waybill";



        public enum AddressType
        {
            NotifyParty,
            NotifyParty2,
            DeliveryAgent,
            ConsignorPickupDeliveryAddress,
            ConsigneePickupDeliveryAddress,
            ConsignorDocumentaryAddress,
            ConsigneeDocumentaryAddress
        }

        public enum DataTarget
        {
            ForwardingConsol,
            ForwardingShipment
        }

        public enum DataTypes
        {
            DateTime,
            String
        }

        public static Dictionary<string, string> CustomizedCollectionKeys = new Dictionary<string, string> {
            {"BillDate","Shipping Bill Date" },
            {"PONumber","P.O. Number" },
            {"ShippingBill","SB/BOE No" }
        };
    }
}
