using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using static ServiceLog.Enums.AuthErrorCodes;
using static ServiceLog.Enums.CategoryErrorCodes;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class DeleteCategoryResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
