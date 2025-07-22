using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceLog.Models.Domain
{
    public class ServiceHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("created_at")]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [BsonElement("device_id")]
        [BsonRequired]
        public string DeviceId { get; set; } 
        [BsonElement("technican_id")]
        [BsonRequired]
        public string TechnicanId { get; set; }
        [BsonElement("ticket_id")]
        [BsonRequired]
        public string TicketId { get; set; }
        [BsonElement("issue_description")]
        [BsonRequired]
        public string IssueDescription { get; set; }
        [BsonElement("other_informations")]
        public string? OtherInformations { get; set; }
        [BsonElement("performed_service_options")]
        [BsonRequired]
        public List<ServiceOption>? PerformedServiceOptions { get; set; }
    }
}
