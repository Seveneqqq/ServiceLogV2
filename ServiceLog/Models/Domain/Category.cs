using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceLog.Models.Domain
{
    public class Category
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; } 

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("service_options")]
        public List<string> ServiceOptions { get; set; } = new();
    }
}
