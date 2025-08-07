using static ServiceLog.Enums.UserErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto
{
    public class BaseResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        
    }
}
