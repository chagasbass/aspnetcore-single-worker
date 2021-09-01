using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Shared.BaseEvents
{
    public class MediatorEvent : IMediatorEvent
    {
        private readonly IMediator _mediator;

        public MediatorEvent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseAsync<T>(T TEvent, CancellationToken cancelationToken) where T : IDomainEvent
        {
            _mediator.Publish(TEvent, cancelationToken);
            return Task.FromResult(0);
        }
    }
}
