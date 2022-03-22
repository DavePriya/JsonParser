using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using Xpediator.Cargowise;

namespace JsonParser.Services.Implementations
{
    public class CargowiseOne : ICargowiseOne
    {
        private readonly CWServiceDetails serviceDetails;

        public CargowiseOne(IOptions<CWServiceDetails> cwDetails)
        {
            serviceDetails = cwDetails.Value;
        }

        public HttpStatusCode UpdateCargowiseOne(MemoryStream mStream)
        {
            var httpXMLClient = new HttpXmlClient(new System.Uri(serviceDetails.eAdapterInboundService),
                                                false,
                                                serviceDetails.CWUserName,
                                                serviceDetails.CWPassword);

            var response = httpXMLClient.Post(mStream);
            return response.StatusCode;
        }
      
    }
}
