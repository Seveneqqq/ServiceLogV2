using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceLog.Models.Domain
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("received_date")]
        public DateTime ReceivedDate { get; set; }

        [BsonElement("resolved_date")]
        public DateTime? ResolvedDate { get; set; }

        [BsonElement("status")]
        [AllowedValues("Open", "In Progress", "Closed")]
        public string Status { get; set; } = string.Empty;

        [BsonElement("devices")]
        public List<string>? DeviceIds { get; set; } = new List<string>();

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [BsonElement("status_history")]
        public List<StatusHistoryEntry>? StatusHistory { get; set; } = new List<StatusHistoryEntry>();

        [BsonElement("technican_id")]
        public string TechnicanId { get; set; } = string.Empty;

        [BsonElement("receiving_method")]
        public string ReceivingMethod { get; set; } = string.Empty;

        [BsonElement("return_method")]
        public string ReturnMethod { get; set; } = string.Empty;
    }

    public class StatusHistoryEntry
    {
        [BsonElement("new_status")]
        [AllowedValues("Open", "In Progress", "Closed")]
        public string NewStatus { get; set; } = string.Empty;

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("technican_id")]
        public string TechnicanId { get; set; } = string.Empty;
    }
}
