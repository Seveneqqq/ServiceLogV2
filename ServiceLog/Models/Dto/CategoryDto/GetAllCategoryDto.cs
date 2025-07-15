using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class GetAllCategoryDto
    {
        [BsonRequired]
        public bool Success { get; set; } = false;
        [BsonRequired]
        public string Message { get; set; } = String.Empty;
        [BsonIgnoreIfNull]
        public List<Category>? Categories { get; set; }
    }
}
