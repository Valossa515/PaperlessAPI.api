using MongoDB.Bson.Serialization.Attributes;
using PaperlessAPI.api.Shared.Attributes;

namespace PaperlessAPI.api.Borders.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("reports")]
    public class DynamicReportEntity(
        string Title,
        IEnumerable<string> ColumnHeaders,
        IEnumerable<IEnumerable<string>> Rows,
        DateTime CreatedAt) : DatabaseEntityBase<Guid>
    {
        public string Title { get; set; } = Title;
        public IEnumerable<string> ColumnHeaders { get; set; } = ColumnHeaders;
        public IEnumerable<IEnumerable<string>> Rows { get; set; } = Rows;
        public DateTime CreatedAt { get; set; } = CreatedAt;
    }
}