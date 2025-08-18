using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class AssignTechnicanToTaskRequestDto
    {
        [Required]
        public string TechnicianId { get; set; } = string.Empty;
    }
}
