using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class ChangeTicketStatusRequestDto
    {
        [Required]
        [AllowedValues("Open", "In Progress", "Closed")]
        public string Status { get; set; } = string.Empty;
    }
}
