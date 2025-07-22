using System.ComponentModel.DataAnnotations;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class UpdateServiceHistoryRequestDto
    {
        [Required]
        public string DeviceId { get; set; } = string.Empty;
        [Required]
        public string TechnicanId { get; set; } = string.Empty;
        [Required]
        public string TicketId { get; set; } = string.Empty;
        [Required]
        public string IssueDescription { get; set; } = string.Empty;
        public string? OtherInformations { get; set; } = null;
        [Required]
        public List<ServiceOption> PerformedServiceOptions { get; set; } = new List<ServiceOption>();
    }
}
