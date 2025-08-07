using System.ComponentModel.DataAnnotations;
using static ServiceLog.Enums.CategoryErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class UpdateCategoryResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
