using System.ComponentModel.DataAnnotations;
using static ServiceLog.Enums.CategoryErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class NewCategoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
