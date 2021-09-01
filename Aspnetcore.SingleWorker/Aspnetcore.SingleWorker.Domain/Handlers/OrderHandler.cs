using Aspnetcore.SingleWorker.Domain.Commands;
using Aspnetcore.SingleWorker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Domain.Handlers
{
    public class OrderHandler : IRequestHandler<OrderCommand, string>
    {
        private readonly IMediator _mediatorEvent;
        private readonly ILogger<OrderHandler> _logger;

        public OrderHandler(IMediator mediatorEvent, ILogger<OrderHandler> logger)
        {
            _mediatorEvent = mediatorEvent;
            _logger = logger;
        }

        public async Task<string> Handle(OrderCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = new OrderEvent("ORDER PROCESSADO");

            request.Status = "ORDER PROCESSANDO";

            _logger.LogError($"Efetuando processamento de pagamento {request.Status}");

            await _mediatorEvent.Publish(orderEvent, cancellationToken);

            return await Task.FromResult(orderEvent.Status);
        }
    }
}
