using Aspnetcore.SingleWorker.CrossCutting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aspnetcore.SingleWorker.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDependencyInjection()
                            .AddOptionsPattern(hostContext.Configuration);

                    services.AddHostedService<Worker>();
                });
    }
}
