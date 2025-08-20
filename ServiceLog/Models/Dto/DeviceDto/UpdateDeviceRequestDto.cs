using static ServiceLog.Enums.DeviceErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.DeviceDto
{
    public class UpdateDeviceRequestDto
    {
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Designation { get; set; }
        public string? Location { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
