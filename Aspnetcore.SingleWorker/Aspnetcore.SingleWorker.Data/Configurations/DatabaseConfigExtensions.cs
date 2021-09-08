using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aspnetcore.SingleWorker.Infrastructure.Data.Configurations
{
    public static class DatabaseConfigExtensions
    {
        public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfigOptions>(configuration.GetSection(DatabaseConfigOptions.BaseConfig));

            return services;
        }
    }
}
