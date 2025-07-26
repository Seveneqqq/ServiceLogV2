using static ServiceLog.Enums.DeviceErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.DeviceDto
{
    public class NewDeviceRequestDto
    {
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Designation { get; set; }
        public string Location { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        [AllowedValues("Active", "Not-active", "In-repair", "Waiting-to-repair", "Unknown")]
        public string Status { get; set; } = "Active";
    }
}
