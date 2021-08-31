using Aspnetcore.SingleWorker.CrossCutting.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private WorkerConfigOptions _workerConfigOptions;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<WorkerConfigOptions> options)
        {
            _workerConfigOptions = options.CurrentValue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(_workerConfigOptions.Runtime, stoppingToken);
            }
        }
    }
}
