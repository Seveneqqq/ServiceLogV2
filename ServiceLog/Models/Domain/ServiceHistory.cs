using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceLog.Models.Domain
{
    public class ServiceHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string DeviceId { get; set; }                  
        public string TechnicanId { get; set; }
        public string TicketId { get; set; }
        public string? OtherInformations { get; set; } 
        public string IssueDescription { get; set; }  
        public List<ServiceOption>? ServiceOptions { get; set; }
    }
}
