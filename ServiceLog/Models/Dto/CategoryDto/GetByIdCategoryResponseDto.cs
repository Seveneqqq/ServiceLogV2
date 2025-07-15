using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.CategoryDto
{
    public class GetByIdCategoryResponseDto
    {
        [BsonRequired]
        public bool Success { get; set; } = false;
        [BsonRequired]
        public string Message { get; set; } = String.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Category? Category { get; set; }
    }
}
