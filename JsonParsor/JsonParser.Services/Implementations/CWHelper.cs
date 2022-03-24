using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace JsonParser.Services.Implementations
{
    public class CWHelper : ICWHelper
    {
         private readonly ICargowiseOne cargowiseOne;

        public CWHelper(ICargowiseOne iCargowiseOne)
        {
            cargowiseOne = iCargowiseOne;
        }

        public bool UpdateShipment(ShipmentModel shipmentModel)
        {
            var success = true;
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("    "),
                CloseOutput = true
            };

            using (var mStream = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(mStream, settings);

                try
                {
                    string xmlNamespace = "http://www.cargowise.com/Schemas/Universal/2011/11";

                    writer.WriteStartElement("UniversalShipment", xmlNamespace);
                    writer.WriteAttributeString("version", "1.1");
                    writer.WriteStartElement("Shipment");
                    writer.WriteStartElement("DataContext");
                    writer.WriteStartElement("DataTargetCollection");

                    writer.WriteStartElement("DataTarget");
                    writer.WriteElementString("Type", Constants.DataTarget.ForwardingConsol.ToString());

                    //writer.WriteStartElement("DataProvider");
                    //writer.WriteString(shipmentModel.ShipmentReferenceNo);
                    //writer.WriteEndElement();//DataProvider end

                    writer.WriteStartElement("EnterpriseID");
                    writer.WriteString(Constants.EnterpriseID);
                    writer.WriteEndElement();//EnterpriseID end

                    writer.WriteElementString("ServerID", Constants.ServerID);

                    writer.WriteEndElement();//data target end  
                    writer.WriteEndElement();//data target collection end  
                    writer.WriteEndElement();//data context end

                    //writer.WriteStartElement("PlaceOfDelivery");
                    //writer.WriteElementString("Code", shipmentModel.PlaceOfDelivery?.Split(',')[0]?.Trim());
                    //writer.WriteElementString("Name", shipmentModel.PlaceOfDelivery?.Split(',')[1]?.Trim());
                    //writer.WriteEndElement();//PlaceOfDelivery end  

                    //writer.WriteStartElement("PortOfDischarge");
                    //writer.WriteElementString("Code", shipmentModel.PlaceOfDischarge?.Split(',')[0]?.Trim());
                    //writer.WriteElementString("Name", shipmentModel.PlaceOfDischarge?.Split(',')[1]?.Trim());
                    //writer.WriteEndElement();//PortOfDischarge end  

                    //writer.WriteStartElement("PortOfLoading");
                    //writer.WriteElementString("Code", shipmentModel.PortOfLoading?.Split(',')[0]?.Trim());
                    //writer.WriteElementString("Name", shipmentModel.PortOfLoading?.Split(',')[1]?.Trim());
                    //writer.WriteEndElement();//PortOfLoading end  

                    writer.WriteElementString("VesselName", shipmentModel.VesselName);
                    writer.WriteElementString("VoyageFlightNo", shipmentModel.VoyageNo);

                    writer.WriteStartElement("ContainerCollection");
                    writer.WriteAttributeString("content", "Complete");
                    writer.WriteStartElement("Container");

                    writer.WriteElementString("ContainerCount", "1");
                    writer.WriteElementString("ContainerNumber", shipmentModel.ContainerNo);

                    writer.WriteStartElement("ContainerType");
                    writer.WriteElementString("Code", shipmentModel.ContainerSize);
                    writer.WriteEndElement();//ContainerType end  

                    writer.WriteElementString("GoodsWeight", shipmentModel.Weight);

                    writer.WriteElementString("Seal", shipmentModel.ShippingLineSeal);

                    writer.WriteStartElement("WeightUnit");
                    writer.WriteElementString("Code", Constants.WeightUnitCode);
                    writer.WriteElementString("Description", Constants.WeightUnitDescription);
                    writer.WriteEndElement();//WeightUnit end  

                    writer.WriteEndElement();//Container end  
                    writer.WriteEndElement();//ContainerCollection end  

                    writer.WriteStartElement("OrganizationAddressCollection");

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty1Address);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty1);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty2.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty2Address);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty2);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteEndElement();//OrganizationAddressCollection end  

                    writer.WriteStartElement("SubShipmentCollection");

                    writer.WriteStartElement("SubShipment");

                    writer.WriteStartElement("DataContext");
                    writer.WriteStartElement("DataTargetCollection");

                    writer.WriteStartElement("DataTarget");
                    writer.WriteElementString("Type", Constants.DataTarget.ForwardingShipment.ToString());
                    writer.WriteEndElement();//data target end  

                    writer.WriteEndElement();//data target collection end  
                    writer.WriteEndElement();//data context end

                    writer.WriteElementString("HBLContainerPackModeOverride", shipmentModel.MoveType);
                    writer.WriteElementString("OuterPacks", shipmentModel.Packages);


                    //writer.WriteStartElement("PortOfDestination");
                    //writer.WriteElementString("Code", shipmentModel.PlaceOfDelivery?.Split(',')[0]?.Trim());
                    //writer.WriteElementString("Name", shipmentModel.PlaceOfDelivery?.Split(',')[1]?.Trim());
                    //writer.WriteEndElement();//PortOfDestination end  

                    //writer.WriteStartElement("PortOfOrigin");
                    //writer.WriteElementString("Code", shipmentModel.PlaceOfAcceptance?.Split(',')[0]?.Trim());
                    //writer.WriteElementString("Name", shipmentModel.PlaceOfAcceptance?.Split(',')[1]?.Trim());
                    //writer.WriteEndElement();//PortOfOrigin end  

                    writer.WriteStartElement("ShipmentIncoTerm");
                    writer.WriteElementString("Code", shipmentModel.PaymentTerm);
                    writer.WriteEndElement();//ShipmentIncoTerm end  

                    writer.WriteElementString("TotalVolume", shipmentModel.Volume?.Split('M')[0]);
                    writer.WriteStartElement("TotalVolumeUnit");
                    writer.WriteElementString("Code", Constants.VolumeUnitCode);
                    writer.WriteElementString("Description", Constants.VolumeUnitDescription);
                    writer.WriteEndElement();//TotalVolumeUnit end  

                    writer.WriteElementString("WayBillNumber", shipmentModel.MTDBLNo);
                    
                    writer.WriteStartElement("LocalProcessing");

                    writer.WriteStartElement("OrderNumberCollection");
                    writer.WriteStartElement("OrderNumber");
                    
                    writer.WriteElementString("OrderReference", shipmentModel.ShipmentReferenceNo);

                    writer.WriteEndElement();//OrderNumber  end
                    writer.WriteEndElement();//OrderNumberCollection end

                    writer.WriteEndElement();//LocalProcessing end

                    writer.WriteStartElement("CustomizedFieldCollection");

                    writer.WriteStartElement("CustomizedField");
                    writer.WriteElementString("DataType", Constants.DataTypes.String.ToString());
                    writer.WriteElementString("Key", Constants.CustomizedCollectionKeys["BillDate"]);
                    writer.WriteElementString("Value", shipmentModel.ShippingBillDate);
                    writer.WriteEndElement();//CustomizedField end

                    writer.WriteStartElement("CustomizedField");
                    writer.WriteElementString("DataType", Constants.DataTypes.String.ToString());
                    writer.WriteElementString("Key", Constants.CustomizedCollectionKeys["ShippingBill"]);
                    writer.WriteElementString("Value", shipmentModel.ShippingBill);
                    writer.WriteEndElement();//CustomizedField end


                    writer.WriteEndElement();//CustomizedFieldCollection end

                    writer.WriteStartElement("OrganizationAddressCollection");

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.DeliveryAgent.ToString());
                    writer.WriteElementString("Address1", shipmentModel.DeliveryAgentContactAddress);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.DeliveryAgentContact);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.ConsignorPickupDeliveryAddress.ToString());
                    writer.WriteElementString("Address1", shipmentModel.ConsignorAddress);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.Consignor);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.ConsigneePickupDeliveryAddress.ToString());
                    writer.WriteElementString("Address1", shipmentModel.ConsigneeAddress);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.Consignee);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteEndElement();//OrganizationAddressCollection end  

                    writer.WriteStartElement("PackingLineCollection");
                    writer.WriteAttributeString("Content", "Complete");

                    foreach (LineItemModel lineItem in shipmentModel.LineItems.Data)
                    {

                        writer.WriteStartElement("PackingLine");
                        writer.WriteElementString("GoodsDescription", lineItem.DescriptionGoods);
                        writer.WriteElementString("HarmonisedCode", shipmentModel.HSCode);
                        writer.WriteElementString("MarksAndNos", lineItem.MarksNumbers);
                        writer.WriteElementString("PackQty", lineItem.NumberPackages?.Split(' ')[0]);

                        writer.WriteElementString("Weight", lineItem.GrossWeight?.Split(' ')[0]);
                        writer.WriteStartElement("WeightUnit");
                        writer.WriteElementString("Code", Constants.WeightUnitCode);
                        writer.WriteElementString("Description", Constants.WeightUnitDescription);
                        writer.WriteEndElement();//WeightUnit end  

                        writer.WriteElementString("Volume", lineItem.Measurement?.Split('M')[0]);
                        writer.WriteStartElement("VolumeUnit");
                        writer.WriteElementString("Code", Constants.VolumeUnitCode);
                        writer.WriteElementString("Description", Constants.VolumeUnitDescription);
                        writer.WriteEndElement();//VolumeUnit end  

                        writer.WriteStartElement("PackType");
                        writer.WriteElementString("Code", Constants.PackageUnitCode);
                        writer.WriteElementString("Description", Constants.PackageUnitDescription);
                        writer.WriteEndElement();//PackType end  

                        writer.WriteStartElement("CustomizedFieldCollection");

                        writer.WriteStartElement("CustomizedField");
                        writer.WriteElementString("DataType", Constants.DataTypes.String.ToString());
                        writer.WriteElementString("Key", Constants.CustomizedCollectionKeys["PONumber"]);
                        writer.WriteElementString("Value", shipmentModel.PONo);
                        writer.WriteEndElement();//CustomizedField end

                        writer.WriteEndElement();//CustomizedFieldCollection 

                        writer.WriteEndElement();//PackingLine end
                    }
                    writer.WriteEndElement();//PackingLineCollection end
                    writer.WriteEndElement();//SubShipment end

                    writer.WriteEndElement();//SubShipmentCollection end

                    writer.WriteEndElement();//shipment end
                    writer.WriteEndElement();//uni shipment end

                    writer.Flush();
                    mStream.Position = 0;

                    //using (FileStream fs = new FileStream("H:\\fileName.xml", FileMode.OpenOrCreate))
                    //{
                    //    mStream.CopyTo(fs);
                    //    fs.Flush();
                    //}


                    var statusCode = cargowiseOne.UpdateCargowiseOne(mStream);
                    if (statusCode != HttpStatusCode.OK) //If response is not OK, set success=false
                    {
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Information("CargowiseHelper Updateshipment" + ex.Message);
                    success = false;
                }
                finally
                {
                    mStream.Flush();
                    writer.Close(); //do not close writer object before writing memorystream object to location. otherwise it will close memorystream objet also
                    writer.Dispose();
                }
                return success;
            }
        }
    }

}
