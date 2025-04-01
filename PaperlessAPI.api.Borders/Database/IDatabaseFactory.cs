using MongoDB.Driver;

namespace PaperlessAPI.api.Borders.Database
{
    public interface IDatabaseFactory
    {
        IMongoDatabase GetDatabase();
        IMongoCollection<T> GetCollection<T>(string name);
        Task<bool> CheckConnectionAsync();
    }
}