using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class NewCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<ServiceOption> ServiceOptions { get; set; }
    }
}
