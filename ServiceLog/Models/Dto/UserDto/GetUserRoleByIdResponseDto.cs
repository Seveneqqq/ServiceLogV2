using System.ComponentModel.DataAnnotations;
using static ServiceLog.Enums.UserErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.UserDto
{
    public class GetUserRoleByIdResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserErrorCode ErrorCode { get; set; } = UserErrorCode.None;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Role { get; set; } = string.Empty;
    }
}
