using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.AuthDto
{
    public class RegisterDto
    {

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string RepeatPassword { get; set; } = string.Empty;

        [Required]
        [AllowedValues("Client", "Technican", "Admin")]
        public string Role { get; set; } = string.Empty;

    }
}
