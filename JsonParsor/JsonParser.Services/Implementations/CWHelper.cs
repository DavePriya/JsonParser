﻿using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using System;
using System.IO;
using System.Xml;

namespace JsonParser.Services.Implementations
{
    public class CWHelper : ICWHelper
    {
        // private readonly ICargowiseOne cargowiseOne;
        //private readonly CWServiceDetails serviceDetails;

        public CWHelper(ICargowiseOne iCargowiseOne/*, IOptions<CWServiceDetails> cwDetails*/)
        {
            //serviceDetails = cwDetails.Value;
            //cargowiseOne = iCargowiseOne;
        }

        public bool UpdateCarbonEmission(ShipmentModel shipmentModel)
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
                    writer.WriteElementString("Type", Constants.DataTarget);

                    writer.WriteStartElement("DataProvider");
                    writer.WriteString(shipmentModel.ShipmentReferenceNo);
                    writer.WriteEndElement();//DataProvider end

                    writer.WriteStartElement("EnterpriseID");
                    writer.WriteString(Constants.EnterpriseID);
                    writer.WriteEndElement();//EnterpriseID end

                    writer.WriteElementString("ServerID", Constants.ServerID);

                    writer.WriteEndElement();//data target end  
                    writer.WriteEndElement();//data target collection end  
                    writer.WriteEndElement();//data context end

                    writer.WriteStartElement("PlaceOfDelivery");
                    writer.WriteElementString("Code", shipmentModel.PlaceOfDelivery?.Split(',')[0]?.Trim());
                    writer.WriteElementString("Name", shipmentModel.PlaceOfDelivery?.Split(',')[1]?.Trim());
                    writer.WriteEndElement();//PlaceOfDelivery end  

                    writer.WriteStartElement("PortOfDischarge");
                    writer.WriteElementString("Code", shipmentModel.PlaceOfDischarge?.Split(',')[0]?.Trim());
                    writer.WriteElementString("Name", shipmentModel.PlaceOfDischarge?.Split(',')[1]?.Trim());
                    writer.WriteEndElement();//PortOfDischarge end  

                    writer.WriteStartElement("PortOfLoading");
                    writer.WriteElementString("Code", shipmentModel.PortOfLoading?.Split(',')[0]?.Trim());
                    writer.WriteElementString("Name", shipmentModel.PortOfLoading?.Split(',')[1]?.Trim());
                    writer.WriteEndElement();//PortOfLoading end  

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



                    writer.WriteStartElement("CustomizedFieldCollection");

                    writer.WriteStartElement("CustomizedField");
                    writer.WriteElementString("DataType", Constants.WeightUnitCode);
                    writer.WriteElementString("Key", Constants.WeightUnitCode);
                    writer.WriteElementString("Key", Constants.WeightUnitCode);
                    writer.WriteEndElement();//CustomizedField end
                 
                    writer.WriteEndElement();//CustomizedFieldCollection end



                    writer.WriteEndElement();//shipment end
                    writer.WriteEndElement();//uni shipment end

                    //writer.Flush();
                    //mStream.Position = 0;

                    //var statusCode = cargowiseOne.UpdateCargowiseOne(mStream);
                    //if (statusCode != HttpStatusCode.OK) //If response is not OK, set success=false
                    //{
                    //    success = false;
                    //}
                }
                catch (Exception ex)
                {
                    // Log.Information("CargowiseHelper UpdateCarbonEmission" + ex.Message);
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
