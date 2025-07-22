using System.Text.Json.Serialization;
using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class CreateServiceHistoryRequestDto
    {
        [Required]
        public string IssueDescription { get; set; }
        public string? OtherInformations { get; set; }
        [Required]
        public string TicketId { get; set; }
        [Required]
        public string TechnicanId { get; set; }
        [Required]
        public string DeviceId { get; set; }
        [Required]
        public List<ServiceOption>? PerformedServiceOptions { get; set; }
    }
}
