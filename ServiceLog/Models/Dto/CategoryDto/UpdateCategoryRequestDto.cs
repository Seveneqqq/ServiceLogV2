using System.ComponentModel.DataAnnotations;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<ServiceOption> ServiceOptions { get; set; } = new List<ServiceOption>();
    }
}
