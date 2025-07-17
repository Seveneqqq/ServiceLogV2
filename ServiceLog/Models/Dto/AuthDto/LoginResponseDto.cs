using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto
{
    public enum AuthErrorCode
    {

        None,
        UserNotFound,
        InvalidPassword,
        EmptyFields,
        Unknown
    }

    public class LoginResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; }
        [Required]
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = "Login failed. Please try again.";
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthErrorCode ErrorCode { get; set; } = AuthErrorCode.None;
    }
}
