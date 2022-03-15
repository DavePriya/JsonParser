using System.IO;
using System.Net;

namespace JsonParser.Services.Interfaces
{
    public interface ICargowiseOne
    {
        HttpStatusCode UpdateCargowiseOne(MemoryStream mStream);
    }
}
