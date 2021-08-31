using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.SingleWorker.CrossCutting.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            return services;
        }
    }
}
