using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;
using static ServiceLog.Enums.CategoryErrorCodes;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class GetByIdCategoryResponseDto
    {
        [Required]
        public bool Success { get; set; } = false;
        [Required]
        public string Message { get; set; } = String.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Category? Category { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
