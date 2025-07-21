using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class ServiceHistoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistoryErrorCode ErrorCode { get; set; } = ServiceHistoryErrorCode.None;
    }
}
