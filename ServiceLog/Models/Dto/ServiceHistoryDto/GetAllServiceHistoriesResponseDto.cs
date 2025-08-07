using static ServiceLog.Enums.ServiceHistoryErrorCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ServiceLog.Models.Domain;

namespace ServiceLog.Models.Dto.ServiceHistoryDto
{
    public class GetAllServiceHistoriesResponseDto : BaseResponseDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<ServiceHistory> ServiceHistories { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ServiceHistoryErrorCode ErrorCode { get; set; } = ServiceHistoryErrorCode.None;
    }
}
