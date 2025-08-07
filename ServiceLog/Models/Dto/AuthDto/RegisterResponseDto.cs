using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceLog.Enums.AuthErrorCodes;

namespace ServiceLog.Models.Dto
{
    public class RegisterResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AuthErrorCode ErrorCode { get; set; } = AuthErrorCode.None;
    }
}
