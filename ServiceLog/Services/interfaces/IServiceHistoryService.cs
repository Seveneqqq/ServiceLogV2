using MongoDB.Driver;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.ServiceHistoryDto;

namespace ServiceLog.Services.interfaces
{
    public interface IServiceHistoryService
    {
        Task<CreateServiceHistoryResponseDto> CreateServiceHistoryAsync(CreateServiceHistoryRequestDto createServiceHistoryRequestDto);
        Task<List<ServiceHistory>> GetAllServiceHistoriesAsync();
        Task<ServiceHistory> GetServiceHistoryByIdAsync(string id);
        Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory);
        Task<DeleteResult> DeleteServiceHistoryAsync(string id);
    }
}
