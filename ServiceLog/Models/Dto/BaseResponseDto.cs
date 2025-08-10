using System.ComponentModel.DataAnnotations;


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

