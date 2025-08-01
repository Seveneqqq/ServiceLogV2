using MongoDB.Driver;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.ServiceHistoryRepository
{
    public interface IServiceHistoryRepository
    {
        Task CreateServiceHistoryAsync(ServiceHistory serviceHistory);
        Task<List<ServiceHistory>> GetAllServiceHistoriesAsync(ServiceHistoryFilter? serviceHistoryFilter);
        Task<ServiceHistory> GetServiceHistoryByIdAsync(string id);
        Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory);
        Task<DeleteResult> DeleteServiceHistoryAsync(string id);
    }
}
