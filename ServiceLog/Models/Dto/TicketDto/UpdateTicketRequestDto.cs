using ServiceLog.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class UpdateTicketRequestDto
    {
        [Required]
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        [AllowedValues("Open", "In Progress", "Closed")]
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? ClientId { get; set; }
        public string? TechnicanId { get; set; }
        public string? ReceivingMethod { get; set; }
        public string? ReturnMethod { get; set; }
    }
}
