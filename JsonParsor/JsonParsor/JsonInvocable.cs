using Coravel.Invocable;
using JsonParser.Services.Interfaces;
using System.Threading.Tasks;

namespace JsonParser
{
    public class JsonInvocable:IInvocable
    {
        private readonly IFileProcessingService fileProcessingService;
        public JsonInvocable(IFileProcessingService iFileProcessingService )
        {
            fileProcessingService = iFileProcessingService;
        }
        public Task Invoke()
        {
            fileProcessingService.ProcessFiles();
            return Task.CompletedTask;
        }
    }
}
