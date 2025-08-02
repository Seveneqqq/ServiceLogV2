using static ServiceLog.Enums.DeviceErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ServiceLog.Enums.TicketErrorCodes;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class CreateTicketResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TicketErrorCode ErrorCode { get; set; } = TicketErrorCode.None;
    }
}
