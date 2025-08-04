using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class CreateTicketRequestDto
    {
        [Required]
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        [Required]
        [AllowedValues("Open", "In Progress", "Closed")]
        public string Status { get; set; }
        [Required]
        public List<Device>? Devices { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string TechnicanId { get; set; }
        [Required]
        public string ReceivingMethod { get; set; }
        [Required]
        public string ReturnMethod { get; set; }
    }
}
