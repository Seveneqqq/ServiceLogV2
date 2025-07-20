using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto
{
    public class RegisterResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; }
        [Required]
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = "Registration failed. Please try again.";

    }
}
