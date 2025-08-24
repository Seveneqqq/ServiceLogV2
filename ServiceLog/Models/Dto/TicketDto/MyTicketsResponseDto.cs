using ServiceLog.Models.Domain;
using static ServiceLog.Enums.TicketErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class MyTicketsResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TicketErrorCode ErrorCode { get; set; } = TicketErrorCode.None;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Ticket>? Tickets { get; set; }
    }
}
