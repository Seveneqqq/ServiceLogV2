using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class NewCategoryRequestDto
    {
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public string Description { get; set; }
        [BsonRequired]
        public List<ServiceOption> ServiceOptions { get; set; }
    }
}
