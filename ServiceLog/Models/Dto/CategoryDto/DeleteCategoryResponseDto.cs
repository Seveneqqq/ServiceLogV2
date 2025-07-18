using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using static ServiceLog.Enums.AuthErrorCodes;
using static ServiceLog.Enums.CategoryErrorCodes;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class DeleteCategoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
