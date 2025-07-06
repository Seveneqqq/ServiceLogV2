using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto
{
    public class RegisterDto
    {

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        public string RepeatPassword { get; set; } = String.Empty;

        [Required]
        [AllowedValues("Client", "Technican", "Admin")]
        public string Role { get; set; } = "Client";

    }
}
