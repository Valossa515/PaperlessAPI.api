using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Borders.Entities;
using PaperlessAPI.api.Shared.Attributes;
using PaperlessAPI.api.Shared.Extensions;
using Serilog;

namespace PaperlessAPI.api.Configuration
{
    public static class DataBaseConfig
    {
        public static async Task<IServiceCollection> ConfigureDatabase(this IServiceCollection services, WebApplication app)
        {
            var databaseFactory = app.Services.GetRequiredService<IDatabaseFactory>();
            var isConnected = await databaseFactory.CheckConnectionAsync();
            if (!isConnected)
            {
                Log.Error("Database configuration not completed.");
                return services;
            }

            ConfigureBsonMapping();
            CreateIndexes(databaseFactory.GetDatabase());

            return services;
        }

        private static void ConfigureBsonMapping()
        {
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        private static void CreateIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<DynamicReportEntity>(typeof(DynamicReportEntity).GetAttributeValue((BsonCollectionAttribute x) => x.CollectionName));
            var reportIdIndex = Builders<DynamicReportEntity>.IndexKeys.Ascending(x => x.Id);
            var titleIndex = Builders<DynamicReportEntity>.IndexKeys.Descending(x => x.Title);
            var createdAtIndex = Builders<DynamicReportEntity>.IndexKeys.Descending(x => x.CreatedAt);
            var columnHeadersIndex = Builders<DynamicReportEntity>.IndexKeys.Descending(x => x.ColumnHeaders);

            collection.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<DynamicReportEntity>(reportIdIndex),
                new CreateIndexModel<DynamicReportEntity>(titleIndex),
                new CreateIndexModel<DynamicReportEntity>(createdAtIndex),
                new CreateIndexModel<DynamicReportEntity>(columnHeadersIndex)
            });
        }
    }
}