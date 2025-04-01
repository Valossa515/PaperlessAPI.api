using MongoDB.Bson.Serialization.Attributes;

namespace PaperlessAPI.api.Borders
{
    public abstract class DatabaseEntityBase<TId> where TId : struct
    {
        [BsonId]
        public TId Id { get; set; }
    }
}