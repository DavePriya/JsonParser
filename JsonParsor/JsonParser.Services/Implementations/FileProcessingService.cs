using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace JsonParser.Services.Implementations
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IFileUtility fileUtility;
        private readonly IConfiguration config;

        public FileProcessingService(IFileUtility fileUtility, IConfiguration iConfig)
        {
            this.fileUtility = fileUtility;
            config = iConfig;
        }

        public void ProcessFiles()
        {
            //  ftpHelper.DownloadSFTPFiles(config["FtpHost"], config["FtpUser"], config["FtpPasssword"], string.Empty, config["InputPath"], false);
            try
            {
                FileSystemInfo[] inputFiles = fileUtility.GetFiles(config["InputPath"], "*.json");
                if (inputFiles?.Length > 0)
                {
                    foreach (FileSystemInfo file in inputFiles)
                    {
                        ShipmentJsonModel shipment = ReadJson<ShipmentJsonModel>(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
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
