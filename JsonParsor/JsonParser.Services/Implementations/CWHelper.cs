using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Text;
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

                    writer.WriteEndElement();//data target end  

                    writer.WriteEndElement();//data target collection end  

                    writer.WriteStartElement("DataSourceCollection");

                    writer.WriteStartElement("DataSource");
                    writer.WriteElementString("Type", Constants.DataTarget.ForwardingConsol.ToString());
                    writer.WriteElementString("Key", shipmentModel.Consol_No);
                    writer.WriteEndElement();//DataSource end  

                    writer.WriteEndElement();//DataSourceCollection end  

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


                    writer.WriteStartElement("ContainerCollection");
                    var containerLink = 1;
                    foreach (LineItem2Model lineItem in shipmentModel.LineItem2.Data)
                    {
                        writer.WriteStartElement("Container");

                        writer.WriteElementString("Link", containerLink.ToString());
                        containerLink++;
                        writer.WriteElementString("ContainerCount", "1");
                        writer.WriteElementString("ContainerNumber", lineItem.Container);

                        writer.WriteStartElement("ContainerType");
                        writer.WriteElementString("Code", lineItem.ContainerSize);
                        writer.WriteEndElement();//ContainerType end  

                        //writer.WriteElementString("GrossWeight", lineItem.Gr_Weight);

                        writer.WriteElementString("Seal", lineItem.ShippingLineSeal);
                        //writer.WriteElementString("VolumeCapacity", lineItem.Volume);

                        //writer.WriteStartElement("WeightUnit");
                        //writer.WriteElementString("Code", Constants.WeightUnitCode);
                        //writer.WriteElementString("Description", Constants.WeightUnitDescription);
                        //writer.WriteEndElement();//WeightUnit end  

                        //writer.WriteStartElement("VolumeUnit");
                        //writer.WriteElementString("Code", Constants.VolumeUnitCode);
                        //writer.WriteElementString("Description", Constants.VolumeUnitDescription);
                        //writer.WriteEndElement();//VolumeUnit end  

                        writer.WriteEndElement();//Container end 
                    }
                    writer.WriteEndElement();//ContainerCollection end  

                    writer.WriteStartElement("OrganizationAddressCollection");

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty1Address);
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty1);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty2.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty2Address);
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty2);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteEndElement();//OrganizationAddressCollection end  

                    writer.WriteStartElement("TransportLegCollection");
                    writer.WriteAttributeString("content", "Complete");

                    writer.WriteStartElement("TransportLeg");

                    writer.WriteStartElement("PortOfDischarge");
                    writer.WriteElementString("Name", shipmentModel.PlaceOfDischarge?.Split(',')[0]?.Trim());
                    writer.WriteEndElement();//PortOfDischarge end  

                    writer.WriteStartElement("PortOfLoading");
                    writer.WriteElementString("Name", shipmentModel.PortOfLoading?.Split(',')[0]?.Trim());
                    writer.WriteEndElement();//PortOfLoading end  

                    writer.WriteElementString("LegOrder", "1");

                    writer.WriteStartElement("BookingStatus");
                    writer.WriteElementString("Code", "CNF");
                    writer.WriteElementString("Description", Constants.BookingStatus);
                    writer.WriteEndElement();//BookingStatus end  

                    writer.WriteElementString("LegType", Constants.LegType);
                    writer.WriteElementString("TransportMode", Constants.TransportMode);
                    writer.WriteElementString("VesselName", shipmentModel.VesselName);
                    writer.WriteElementString("VoyageFlightNo", shipmentModel.VoyageNo);

                    writer.WriteEndElement();//TransportLeg end  

                    writer.WriteEndElement();//TransportLegCollection end  

                    writer.WriteStartElement("SubShipmentCollection");

                    writer.WriteStartElement("SubShipment");

                    writer.WriteStartElement("DataContext");
                    writer.WriteStartElement("DataTargetCollection");

                    writer.WriteStartElement("DataTarget");
                    writer.WriteElementString("Type", Constants.DataTarget.ForwardingShipment.ToString());
                    writer.WriteElementString("Key", shipmentModel.MTDBLNo);
                    writer.WriteEndElement();//data target end  

                    writer.WriteEndElement();//data target collection end  
                    writer.WriteEndElement();//data context end

                    //int packages = 0;
                    //foreach (LineItem2Model lineItem in shipmentModel.LineItem2.Data)
                    //{
                    //    int.TryParse(lineItem.Packages, out int lineItemPackages);
                    //    if (lineItemPackages > 0)
                    //    {
                    //        packages += lineItemPackages;
                    //    }
                    //}

                    writer.WriteElementString("OuterPacks", shipmentModel.LineItem1.Data[0]?.NumberPackages);

                    writer.WriteStartElement("OuterPacksPackageType");
                    writer.WriteElementString("Code", shipmentModel.LineItem1.Data[0]?.Pkg_Type);
                    writer.WriteEndElement();//OuterPacksPackageType end  

                    writer.WriteElementString("HBLContainerPackModeOverride", shipmentModel.MoveType);

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

                    writer.WriteStartElement("ReleaseType");
                    writer.WriteElementString("Code", shipmentModel.ReleaseType);
                    writer.WriteEndElement();//ReleaseType end  

                    writer.WriteElementString("TotalVolume", shipmentModel.LineItem1.Data[0].Measurement);

                    writer.WriteStartElement("TotalVolumeUnit");
                    writer.WriteElementString("Code", shipmentModel.LineItem1.Data[0].Measurement_Units);
                    writer.WriteEndElement();//TotalVolumeUnit end  

                    writer.WriteElementString("TotalWeight", shipmentModel.LineItem1.Data[0].GrossWeight);

                    writer.WriteStartElement("TotalWeightUnit");
                    writer.WriteElementString("Code", shipmentModel.LineItem1.Data[0].Gr_Units);
                    writer.WriteEndElement();//TotalWeightUnit end  

                    writer.WriteElementString("WayBillNumber", shipmentModel.MTDBLNo);
                    writer.WriteStartElement("WayBillType");
                    writer.WriteElementString("Code", Constants.WayBillTypeCode);
                    writer.WriteElementString("Description", Constants.WayBillTypeDesc);
                    writer.WriteEndElement();//WayBillType end  

                    writer.WriteStartElement("LocalProcessing");

                    writer.WriteStartElement("OrderNumberCollection");
                    var shipmentRefs = shipmentModel.ShipmentReferenceNo.Split(',');

                    for (int i = 0; i < shipmentRefs.Length; i++)
                    {
                        writer.WriteStartElement("OrderNumber");

                        writer.WriteElementString("OrderReference", shipmentRefs[i]);
                        writer.WriteElementString("Sequence", (i + 1).ToString());

                        writer.WriteEndElement();//OrderNumber  end
                    }
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

                    writer.WriteStartElement("DateCollection");
                    writer.WriteStartElement("Date");
                    writer.WriteElementString("Type", Constants.BillIssued);
                    writer.WriteElementString("IsEstimate", false.ToString());
                    writer.WriteElementString("Value", shipmentModel.DateofIssue);
                    writer.WriteEndElement();//Date end
                    writer.WriteEndElement();//DateCollection end


                    writer.WriteStartElement("NoteCollection");
                    writer.WriteElementString("Content", "Partial");

                    writer.WriteStartElement("Note");
                    writer.WriteElementString("Description", "Marks & Numbers");
                    writer.WriteStartElement("IsCustomDescription"); //IsCustomDescription started
                    writer.WriteString("false");
                    writer.WriteEndElement();//IsCustomDescription end
                    writer.WriteStartElement("NoteText"); //NoteText started
                    writer.WriteString(shipmentModel.LineItem1.Data[0]?.MarksNumbers.ToString());
                    writer.WriteEndElement();//NoteText end

                    writer.WriteStartElement("NoteContext"); //NoteContext started

                    writer.WriteStartElement("Code"); //Code started
                    writer.WriteString("AAA");
                    writer.WriteEndElement();//Code end

                    writer.WriteStartElement("Description"); //Description started
                    writer.WriteString("Module: A - All, Direction: A - All, Freight: A - All");
                    writer.WriteEndElement();//Description end

                    writer.WriteEndElement();//NoteContext end

                    writer.WriteStartElement("Visibility"); //Visibility started

                    writer.WriteStartElement("Code"); //Code started
                    writer.WriteString("PUB");
                    writer.WriteEndElement();//Code end

                    writer.WriteStartElement("Description"); //Description started
                    writer.WriteString("CLIENT-VISIBLE");
                    writer.WriteEndElement();//Description end

                    writer.WriteEndElement();//Visibility end

                    writer.WriteEndElement();//Note end 


                    //var noteText = new StringBuilder(string.Empty);
                    //foreach (LineItemModel lineItem in shipmentModel.LineItem1.Data)
                    //{
                    //    noteText.AppendJoin(',', lineItem.MarksNumbers);
                    //}

                    //var noteText = new StringBuilder(string.Empty);
                    //foreach (LineItemModel lineItem in shipmentModel.LineItem1.Data)
                    //{
                    //    noteText.AppendJoin(',', lineItem.MarksNumbers);
                    //}
                    //writer.WriteString(noteText.ToString());

                    writer.WriteStartElement("Note");
                    writer.WriteElementString("Description", "Detailed Goods Description");
                    writer.WriteStartElement("IsCustomDescription"); //IsCustomDescription started
                    writer.WriteString("false");
                    writer.WriteEndElement();//IsCustomDescription end

                    writer.WriteStartElement("NoteText"); //NoteText started
                    writer.WriteString(shipmentModel.LineItem1.Data[0]?.DescriptionGoods);
                    writer.WriteEndElement();//NoteText end

                    writer.WriteStartElement("NoteContext"); //NoteContext started

                    writer.WriteStartElement("Code"); //Code started
                    writer.WriteString("AAA");
                    writer.WriteEndElement();//Code end

                    writer.WriteStartElement("Description"); //Description started
                    writer.WriteString("Module: A - All, Direction: A - All, Freight: A - All");
                    writer.WriteEndElement();//Description end

                    writer.WriteEndElement();//NoteContext end

                    writer.WriteStartElement("Visibility"); //Visibility started

                    writer.WriteStartElement("Code"); //Code started
                    writer.WriteString("PUB");
                    writer.WriteEndElement();//Code end

                    writer.WriteStartElement("Description"); //Description started
                    writer.WriteString("CLIENT-VISIBLE");
                    writer.WriteEndElement();//Description end

                    writer.WriteEndElement();//Visibility end

                    writer.WriteEndElement();//Note end 

                    //noteText = new StringBuilder(string.Empty);
                    //foreach (LineItemModel lineItem in shipmentModel.LineItem1.Data)
                    //{
                    //    noteText.AppendJoin(',', lineItem.DescriptionGoods);
                    //}
                    //writer.WriteString(noteText.ToString());

                    writer.WriteEndElement();//NoteCollection end 

                    writer.WriteStartElement("OrganizationAddressCollection");

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.DeliveryAgent.ToString());
                    writer.WriteElementString("Address1", shipmentModel.DeliveryAgentContactAddress);
                    writer.WriteElementString("CompanyName", shipmentModel.DeliveryAgentContact);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.ConsignorDocumentaryAddress.ToString());
                    writer.WriteElementString("Address1", shipmentModel.ConsignorAddress);
                    writer.WriteElementString("CompanyName", shipmentModel.Consignor);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.ConsigneeDocumentaryAddress.ToString());
                    writer.WriteElementString("Address1", shipmentModel.ConsigneeAddress);
                    writer.WriteElementString("CompanyName", shipmentModel.Consignee);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty1Address);
                    writer.WriteElementString("AddressOverride", "false");
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty1);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteStartElement("OrganizationAddress");
                    writer.WriteElementString("AddressType", Constants.AddressType.NotifyParty2.ToString());
                    writer.WriteElementString("Address1", shipmentModel.NotifyParty2Address);
                    writer.WriteElementString("CompanyName", shipmentModel.NotifyParty2);
                    writer.WriteEndElement();//OrganizationAddress end  

                    writer.WriteEndElement();//OrganizationAddressCollection end  

                    writer.WriteStartElement("PackingLineCollection");
                    containerLink = 1;

                    foreach (LineItem2Model lineItem in shipmentModel.LineItem2.Data)
                    {
                        writer.WriteStartElement("PackingLine");

                        writer.WriteElementString("ContainerNumber", lineItem.Container);
                        writer.WriteElementString("Link", containerLink.ToString());
                        writer.WriteElementString("ContainerLink", containerLink.ToString());

                        containerLink++;

                        writer.WriteElementString("PackQty", lineItem.Packages);
                        writer.WriteElementString("HarmonisedCode", lineItem.HSCode);

                        writer.WriteElementString("Weight", lineItem.Gr_Weight);

                        writer.WriteStartElement("WeightUnit");
                        writer.WriteElementString("Code", lineItem.GR_Units);
                        writer.WriteEndElement();//WeightUnit end  

                        writer.WriteElementString("Volume", lineItem.Volume);
                        writer.WriteStartElement("VolumeUnit");
                        writer.WriteElementString("Code", lineItem.Volume_Units);
                        writer.WriteEndElement();//VolumeUnit end  

                        writer.WriteStartElement("PackType");
                        writer.WriteElementString("Code", lineItem.Packages_Units);
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

                    using (FileStream fs = new FileStream("H:\\fileName.xml", FileMode.OpenOrCreate))
                    {
                        mStream.CopyTo(fs);
                        fs.Flush();
                    }

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
