using Aspnetcore.SingleWorker.Domain.Entities;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Domain.Services
{
    public interface IOrderQueueService
    {
        Task<Order> ReadQueueAsync();
        Task DeleteMessageAsync(object message);
    }
}
