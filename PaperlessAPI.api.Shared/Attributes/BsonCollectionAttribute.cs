namespace PaperlessAPI.api.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BsonCollectionAttribute(string collectionName) : Attribute
    {
        public string CollectionName { get; } = collectionName;
    }
}