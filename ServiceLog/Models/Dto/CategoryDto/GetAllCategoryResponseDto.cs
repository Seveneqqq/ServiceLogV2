using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;
using static ServiceLog.Enums.CategoryErrorCodes;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class GetAllCategoryResponseDto : BaseResponseDto
    {
        [Required]
        public List<Category>? Categories { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CategoryErrorCode ErrorCode { get; set; } = CategoryErrorCode.None;
    }
}
