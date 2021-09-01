using Aspnetcore.SingleWorker.Domain.Entities;
using Aspnetcore.SingleWorker.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Aspnetcore.SingleWorker.Infrastructure.Data.Contexts
{
    public class DbContext : IDbContext
    {
        public IMongoDatabase _mongoDatabase;

        MongoClientSettings _mongoClientSettings;
        MongoClient _mongoClient;

        private DatabaseConfigOptions _databaseConfigOptions;

        public DbContext(IOptionsMonitor<DatabaseConfigOptions> options)
        {
            _databaseConfigOptions = options.CurrentValue;

            Connect();
        }

        public void Connect()
        {
            _mongoClientSettings = MongoClientSettings.FromConnectionString(_databaseConfigOptions.ConnectionString);
            _mongoClient = new MongoClient(_mongoClientSettings);
            _mongoDatabase = _mongoClient.GetDatabase(_databaseConfigOptions.DatabaseName);
        }

        #region Coleções
        public IMongoCollection<Order> Orders => _mongoDatabase.GetCollection<Order>("Orders");
        #endregion
    }
}