using System.ComponentModel.DataAnnotations;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class AddDevicesToTicketRequestDto
    {
        [Required]
        public List<string> DeviceIds { get; set; } = new List<string>();
    }
}
