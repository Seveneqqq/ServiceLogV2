using static ServiceLog.Enums.UserErrorCodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ServiceLog.Models.Dto.UserDto
{
    public class GetAllUsersResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserErrorCode ErrorCode { get; set; } = UserErrorCode.None;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<IdentityUser> Users { get; set; } = new List<IdentityUser>();
    }
}
