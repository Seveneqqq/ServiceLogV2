using static ServiceLog.Enums.TicketErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class GetTicketByIdResponseDto
    {
        [Required]
        public bool Success { get; set; }
        [Required]
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TicketErrorCode ErrorCode { get; set; } = TicketErrorCode.None;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Ticket Ticket { get; set; }
    }
}
