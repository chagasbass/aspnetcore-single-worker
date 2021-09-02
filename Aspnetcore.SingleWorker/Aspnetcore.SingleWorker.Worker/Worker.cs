using Aspnetcore.SingleWorker.CrossCutting.Configurations;
using Aspnetcore.SingleWorker.CrossCutting.Extensions;
using Aspnetcore.SingleWorker.Domain.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMediator _mediator;

        private readonly WorkerConfigOptions _workerConfigOptions;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory,
                      IOptions<WorkerConfigOptions> options, IMediator mediator)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _mediator = mediator;

            _workerConfigOptions = options.Value;
        }

        private void ReloadOptions()
        {
            var runtimeConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var value = runtimeConfiguration["WorkerConfig:Runtime"];
            _workerConfigOptions.Runtime = Int32.Parse(value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //faz o dispose de todos os serviços declarados como scopped a cada rodada de leitura
                using var scope = _serviceScopeFactory.CreateScope();

                /*vai na fila
                 *cria comando
                 *processa (montar e acessar o banco)
                 *gera evento
                 *FIM
                 */

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var orderCommand = new OrderCommand
                {
                    Status = "ORDER CHEGOU"
                };

                _logger.LogError($"CHEGADA DE ORDER PARA PROCESSAMENTO: {orderCommand.Status}");

                await _mediator.Send(orderCommand);

                _logger.LogError($"FIM DO PROCESSAMENTO DO ORDER");

                await Task.Delay(_workerConfigOptions.Runtime, stoppingToken);

                _workerConfigOptions.ReloadOptions();
            }
        }
    }
}
