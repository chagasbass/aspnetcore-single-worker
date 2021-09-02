using Aspnetcore.SingleWorker.CrossCutting.Configurations;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Aspnetcore.SingleWorker.CrossCutting.Extensions
{
    public static class WorkerConfigOptionsExtensions
    {
        public static void ReloadOptions(this WorkerConfigOptions workerConfigOptions)
        {
            var runtimeConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var value = runtimeConfiguration["WorkerConfig:Runtime"];
            workerConfigOptions.Runtime = Int32.Parse(value);
        }
    }
}
