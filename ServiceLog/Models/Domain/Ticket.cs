using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceLog.Models.Domain
{
    public class Ticket
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("received_date")]
        public DateTime ReceivedDate { get; set; }

        [BsonElement("resolved_date")]
        public DateTime? ResolvedDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("devices")]
        public List<string> Devices { get; set; }  

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("client_id")]
        public Guid ClientId { get; set; }

        [BsonElement("status_history")]
        public List<StatusHistoryEntry> StatusHistory { get; set; } = new List<StatusHistoryEntry>();

        [BsonElement("technicant_id")]
        public Guid TechnicantId { get; set; }

        [BsonElement("receiving_method")]
        public string ReceivingMethod { get; set; }

        [BsonElement("return_method")]
        public string ReturnMethod { get; set; }
    }

    public class StatusHistoryEntry
    {
        [BsonElement("new_status")]
        public string NewStatus { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("technicant_id")]
        public Guid TechnicantId { get; set; }
    }
}
