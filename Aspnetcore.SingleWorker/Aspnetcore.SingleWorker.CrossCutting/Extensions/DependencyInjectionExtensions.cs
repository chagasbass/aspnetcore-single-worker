using Aspnetcore.SingleWorker.Domain.EventHandlers;
using Aspnetcore.SingleWorker.Domain.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.SingleWorker.CrossCutting.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //services.AddScoped<DbContext, DbContext>();
            //services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMediatR(typeof(OrderHandler).Assembly);
            services.AddMediatR(typeof(OrderEventHandler).Assembly);

            return services;
        }
    }
}
