namespace JsonParser.Services
{
    public class Constants
    {
        public const string EnterpriseID = "TIP";
        public const string ServerID = "TS3";
        public const string WeightUnitCode = "KG";
        public const string WeightUnitDescription = "Kilograms";
        public const string VolumeUnitCode = "M3";
        public const string VolumeUnitDescription = "Cubic Metres";


        public enum AddressType
        {
            NotifyParty,
            NotifyParty2,
            DeliveryAgent,
            ConsignorPickupDeliveryAddress,
            ConsigneePickupDeliveryAddress
        }

        public enum DataTarget
        {
            ForwardingConsol,
            ForwardingShipment
        }



    }
}
