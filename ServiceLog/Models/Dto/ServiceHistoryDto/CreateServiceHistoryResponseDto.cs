using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class CreateServiceHistoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistoryErrorCode ErrorCode { get; set; } = ServiceHistoryErrorCode.None;
    }
}
