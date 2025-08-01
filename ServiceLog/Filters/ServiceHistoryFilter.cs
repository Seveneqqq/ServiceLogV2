using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ServiceLog.Models.Domain;

namespace ServiceLog.Filters
{
    public class ServiceHistoryFilter
    {
        public string? Id { get; set; }
        public DateTime? Date { get; set; }
        public string? DeviceId { get; set; }
        public string? TechnicanId { get; set; }
        public string? TicketId { get; set; }
        public string? IssueDescription { get; set; }
        public string? OtherInformations { get; set; }
    }
}
