using Aspnetcore.SingleWorker.CrossCutting.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.SingleWorker.CrossCutting.Extensions
{
    public static class OptionsPatternExtensions
    {
        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WorkerConfigOptions>(configuration.GetSection(WorkerConfigOptions.BaseConfig));
            services.Configure<QueueConfigOptions>(configuration.GetSection(QueueConfigOptions.BaseConfig));

            return services;
        }
    }
}
