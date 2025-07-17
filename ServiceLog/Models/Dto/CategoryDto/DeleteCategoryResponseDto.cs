using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class DeleteCategoryResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
