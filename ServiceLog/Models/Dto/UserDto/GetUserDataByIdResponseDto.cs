using static ServiceLog.Enums.UserErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.UserDto
{
    public class GetUserDataByIdResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserErrorCode ErrorCode { get; set; } = UserErrorCode.None;
    }
}
