using Aspnetcore.SingleWorker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Domain.EventHandlers
{
    public class OrderEventHandler : INotificationHandler<OrderEvent>
    {
        private readonly ILogger<OrderEventHandler> _logger;

        public OrderEventHandler(ILogger<OrderEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(OrderEvent notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
          {
              _logger.LogError($"EVENTO DE PROCESSAMENTO DE ORDER Status:{notification.Status}, Data = {notification.Timestamp}");
          });
        }
    }
}
