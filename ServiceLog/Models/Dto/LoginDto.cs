using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto
{
    public class LoginDto
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
