using Aspnetcore.SingleWorker.CrossCutting.Extensions;
using Aspnetcore.SingleWorker.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Aspnetcore.SingleWorker.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IConfiguration GetConfiguration(string[] args, IHostEnvironment environment)
        {
            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment;

                    var config = GetConfiguration(args, env);

                    services.AddDependencyInjection()
                            .AddOptionsPattern(config)
                            .AddDatabaseConfigurations(config);

                    services.AddHostedService<Worker>();
                });
    }
}
