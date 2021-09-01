using Aspnetcore.SingleWorker.Domain.Entities;
using Aspnetcore.SingleWorker.Domain.Repositories;
using Aspnetcore.SingleWorker.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext _context;

        public OrderRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await _context.Orders.ReplaceOneAsync(p => p.Id == order.Id, order);
            return order;
        }
    }
}
