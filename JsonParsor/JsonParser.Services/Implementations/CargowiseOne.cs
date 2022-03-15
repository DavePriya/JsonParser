using JsonParser.Services.Interfaces;
using System;
using System.IO;
using System.Net;

namespace JsonParser.Services.Implementations
{
    public class CargowiseOne : ICargowiseOne
    {
        //    private readonly CWServiceDetails serviceDetails;

        //    public CargowiseOne(IOptions<CWServiceDetails> cwDetails)
        //    {
        //        serviceDetails = cwDetails.Value;
        //    }

        //    public HttpStatusCode UpdateCargowiseOne(MemoryStream mStream)
        //    {
        //        var httpXMLClient = new HttpXmlClient(new System.Uri(serviceDetails.eAdapterInboundService),
        //                                            false,
        //                                            serviceDetails.CWUserName,
        //                                            serviceDetails.CWPassword);

        //        var response = httpXMLClient.Post(mStream);
        //        return response.StatusCode;
        //    }
        public HttpStatusCode UpdateCargowiseOne(MemoryStream mStream)
        {
            throw new NotImplementedException();
        }
    }
}
