using Aspnetcore.SingleWorker.Domain.Entities;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> UpdateOrderAsync(Order order);
    }
}
