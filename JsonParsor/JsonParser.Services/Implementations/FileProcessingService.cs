using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;

namespace JsonParser.Services.Implementations
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IFileUtility fileUtility;
        private readonly IConfiguration config;
        private readonly IFtpHelper ftpHelper;
        private readonly ICWHelper cwHelper;
        public FileProcessingService(IFileUtility fileUtility, IConfiguration iConfig,
            IFtpHelper iFtpHelper, ICWHelper iCWHelper)
        {
            this.fileUtility = fileUtility;
            config = iConfig;
            ftpHelper = iFtpHelper;
            cwHelper = iCWHelper;
        }

        public void ProcessFiles()
        {
            try
            {
               ftpHelper.DownloadSFTPFiles(config["FtpHost"], config["FtpUser"], config["FtpPasssword"], config["FtpDir"], config["InputPath"],config["DeleteFileAfterDownload"]);

                FileSystemInfo[] inputFiles = fileUtility.GetFiles(config["InputPath"], "*.json");
                if (inputFiles?.Length > 0)
                {
                    foreach (FileSystemInfo file in inputFiles)
                    {
                        try
                        {
                            ShipmentJsonModel shipment = ReadJson<ShipmentJsonModel>(file.FullName);
                        if (cwHelper.UpdateShipment(shipment.Data))
                        {
                            fileUtility.MoveFileTo(config["ProcessedFiles"], file.FullName);
                        }
                        else
                        {
                            fileUtility.MoveFileTo(config["ErrorFiles"], file.FullName);
                        }
                        }
                        catch (Exception ex)
                        {
                            Log.Information("FileprocesingService ProcessFiles Outer exception " + file.FullName + " ===> " + ex.Message + "  " + ex.InnerException + " " + ex.StackTrace);
                            fileUtility.MoveFileTo(config["ErrorFiles"], file.FullName);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Information("FileprocesingService ProcessFiles Outer exception " + ex.Message + "  " + ex.InnerException + " " + ex.StackTrace);
            }
        }

        public T ReadJson<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //ro = ser.Deserialize<RootObject>(jsonString);

            //  return JsonSerializer.Deserialize<T>(text);
        }
    }
}
