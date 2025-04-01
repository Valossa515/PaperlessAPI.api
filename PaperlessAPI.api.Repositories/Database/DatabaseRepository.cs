using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PaperlessAPI.api.Borders;
using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Shared.Attributes;
using PaperlessAPI.api.Shared.Extensions;

namespace PaperlessAPI.api.Repositories.Database
{
    public abstract class DatabaseRepository<TEntity, TId>(
        IDatabaseFactory databaseFactory,
        ILogger logger) where TEntity : DatabaseEntityBase<TId> where TId : struct
    {
        protected readonly ILogger _logger = logger;
        protected readonly IMongoCollection<TEntity> _collection = databaseFactory
            .GetDatabase()
            .GetCollection<TEntity>(typeof(TEntity)
            .GetAttributeValue((BsonCollectionAttribute x) => x.CollectionName));

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            var filter = Builders<TEntity>.Filter.Eq(m => m.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
           => await _collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();

        public async Task DeleteAllAsync()
            => await _collection.DeleteManyAsync(Builders<TEntity>.Filter.Empty);

        public async Task InsertAllAsync(IEnumerable<TEntity> documents)
            => await _collection.InsertManyAsync(documents);
    }
}