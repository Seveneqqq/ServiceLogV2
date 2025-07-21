using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class GetByIdServiceHistoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistory ServiceHistory { get; set; };
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistoryErrorCode ErrorCode { get; set; } = ServiceHistoryErrorCode.None;
    }
}
