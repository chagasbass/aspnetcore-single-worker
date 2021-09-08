namespace Aspnetcore.SingleWorker.Infrastructure.Data.Configurations
{
    public class DatabaseConfigOptions
    {
        public const string BaseConfig = "DatabaseConfig";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
