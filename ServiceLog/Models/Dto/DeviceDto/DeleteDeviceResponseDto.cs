using System.ComponentModel.DataAnnotations;
using static ServiceLog.Enums.CategoryErrorCodes;
using System.Text.Json.Serialization;
using static ServiceLog.Enums.DeviceErrorCodes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.DeviceDto
{
    public class DeleteDeviceResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DeviceErrorCode ErrorCode { get; set; } = DeviceErrorCode.None;
        
    }
}
