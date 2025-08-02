using MongoDB.Driver;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.ServiceHistoryDto;

namespace ServiceLog.Services.interfaces
{
    public interface IServiceHistoryService
    {
        Task<CreateServiceHistoryResponseDto> CreateServiceHistoryAsync(CreateServiceHistoryRequestDto createServiceHistoryRequestDto);
        Task<GetAllServiceHistoriesResponseDto> GetAllServiceHistoriesAsync(ServiceHistoryFilter? serviceHistoryFilter);
        Task<GetByIdServiceHistoryResponseDto> GetServiceHistoryByIdAsync(string id);
        Task<ServiceHistoryResponseDto> UpdateServiceHistoryAsync(string id, UpdateServiceHistoryRequestDto updateServiceHistoryRequestDto);
        Task<ServiceHistoryResponseDto> DeleteServiceHistoryAsync(string id);
    }
}
