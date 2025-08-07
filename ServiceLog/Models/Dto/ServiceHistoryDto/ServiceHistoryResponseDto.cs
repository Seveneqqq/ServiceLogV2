using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class ServiceHistoryResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistoryErrorCode ErrorCode { get; set; } = ServiceHistoryErrorCode.None;
    }
}
