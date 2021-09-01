using System.Threading;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Shared.BaseEvents
{
    public interface IMediatorEvent
    {
        Task RaiseAsync<T>(T TEvent, CancellationToken cancelationToken) where T : IDomainEvent;
    }
}
