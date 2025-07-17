using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class GetAllCategoryDto
    {
        [Required]
        public bool Success { get; set; } = false;
        [Required]
        public string Message { get; set; } = String.Empty;
        [Required]
        public List<Category>? Categories { get; set; }
    }
}
