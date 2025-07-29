using static ServiceLog.Enums.DeviceErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.DeviceDto
{
    public class GetByIdDeviceResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DeviceErrorCode ErrorCode { get; set; } = DeviceErrorCode.None;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Device Device { get; set; }
    }
}
