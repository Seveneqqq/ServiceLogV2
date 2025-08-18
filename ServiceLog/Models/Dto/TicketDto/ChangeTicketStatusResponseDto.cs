using static ServiceLog.Enums.TicketErrorCodes;
using System.Text.Json.Serialization;

namespace ServiceLog.Models.Dto.TicketDto
{
    public class ChangeTicketStatusResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TicketErrorCode ErrorCode { get; set; } = TicketErrorCode.None;
    }
}
