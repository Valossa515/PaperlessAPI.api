using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Shared.Exceptions;
using PaperlessAPI.api.Shared.Properties;

namespace PaperlessAPI.api.Repositories.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger _logger;
        public DatabaseFactory(ApplicationConfig applicationConfig, ILogger<DatabaseFactory> logger)
        {
            try
            {
                var mongoClient = new MongoClient(applicationConfig.ConnectionStrings.DefaultConnection);
                _database = mongoClient.GetDatabase(applicationConfig.ConnectionStrings.DatabaseName);
                _logger = logger;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{ErrorMessage}", Resources.DatabaseConnectionError);
                throw new DatabaseException(Resources.DatabaseConnectionError, ex);
            }
        }

        public IMongoDatabase GetDatabase() => _database;

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);

        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ErrorMessage}", Resources.DatabaseConnectionError);
                return false;
            }
        }
    }
}