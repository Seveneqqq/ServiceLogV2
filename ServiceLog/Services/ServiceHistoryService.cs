using MongoDB.Driver;
using ServiceLog.Models.Domain;
using ServiceLog.Repositories.ServiceHistoryRepository;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Services
{
    public class ServiceHistoryService : IServiceHistoryService
    {

        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public ServiceHistoryService(IServiceHistoryRepository serviceHistoryRepository)
        {
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        public Task CreateServiceHistoryAsync(ServiceHistory serviceHistory)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteServiceHistoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ServiceHistory>> GetAllServiceHistoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceHistory> GetServiceHistoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory)
        {
            throw new NotImplementedException();
        }
    }
}
