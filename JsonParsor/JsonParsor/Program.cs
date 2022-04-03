using Coravel;
using JsonParser.Services.Implementations;
using JsonParser.Services.Interfaces;
using JsonParser.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JsonParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler =>
            {
                scheduler.Schedule<JsonInvocable>().EveryFiveMinutes().PreventOverlapping("JsonInvocable");
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args).UseWindowsService()
                   .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
                   .ConfigureServices((hostContext, services) =>
                   {
                       var configuration = hostContext.Configuration;
                       services.AddScheduler();
                      // services.AddHttpClient();
                       services.AddTransient<JsonInvocable>();
                       services.AddLogging(configure => configure.AddSerilog());
                       services.AddTransient<IFileProcessingService, FileProcessingService>();
                       services.Configure<CWServiceDetails>(configuration.GetSection("CWService"));
                       services.AddTransient<ICargowiseOne, CargowiseOne>();
                       services.AddTransient<ICWHelper, CWHelper>();
                       services.AddTransient<IFileUtility, FileUtility>();
                       services.AddTransient<IFtpHelper, FtpHelper>();

                   });
    }
}
