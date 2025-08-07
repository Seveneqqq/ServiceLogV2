using static ServiceLog.Enums.CategoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceLog.Enums.UserErrorCodes;

namespace ServiceLog.Models.Dto.UserDto
{
    public class DeleteUserByIdResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserErrorCode ErrorCode { get; set; } = UserErrorCode.None;
    }
}
