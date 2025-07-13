using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.AuthDto
{
    public class LoginDto
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
