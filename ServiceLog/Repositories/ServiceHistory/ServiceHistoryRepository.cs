using MongoDB.Bson;
using MongoDB.Driver;
using ServiceLog.Data;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.ServiceHistoryRepository
{
    public class ServiceHistoryRepository : IServiceHistoryRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public ServiceHistoryRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task CreateServiceHistoryAsync(ServiceHistory serviceHistory)
        {
            await _mongoDbContext.ServiceHistories.InsertOneAsync(serviceHistory);
        }

        public async Task<DeleteResult> DeleteServiceHistoryAsync(string id)
        {
            var filter = Builders<ServiceHistory>.Filter.Eq("_id", new ObjectId(id));
            return await _mongoDbContext.ServiceHistories.DeleteOneAsync(filter);
        }

        public async Task<List<ServiceHistory>> GetAllServiceHistoriesAsync()
        {
           return await _mongoDbContext.ServiceHistories.Find(_ => true).ToListAsync();
        }

        public async Task<ServiceHistory> GetServiceHistoryByIdAsync(string id)
        {
            return await _mongoDbContext.ServiceHistories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory)
        {
            return await _mongoDbContext.ServiceHistories.ReplaceOneAsync(x => x.Id == id, serviceHistory);
        }
    }
}
