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
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("description")]
        [BsonRequired]
        public string Description { get; set; }

        [BsonElement("service_options")]
        [BsonRequired]
        public List<ServiceOption> ServiceOptions { get; set; }
    }
}
