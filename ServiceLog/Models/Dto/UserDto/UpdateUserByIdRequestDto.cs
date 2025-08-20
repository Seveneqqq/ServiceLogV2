using System.ComponentModel.DataAnnotations;

namespace ServiceLog.Models.Dto.UserDto
{
    public class UpdateUserByIdRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
