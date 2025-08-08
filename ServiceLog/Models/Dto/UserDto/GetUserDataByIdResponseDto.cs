using static ServiceLog.Enums.UserErrorCodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ServiceLog.Models.Dto.UserDto
{
    public class GetUserDataByIdResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IdentityUser User { get; set; } = new IdentityUser();
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserErrorCode ErrorCode { get; set; } = UserErrorCode.None;
    }
}
