﻿using MongoDB.Driver;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.ServiceHistoryRepository
{
    public interface IServiceHistoryRepository
    {
        Task CreateServiceHistoryAsync(ServiceHistory serviceHistory);
        Task<List<ServiceHistory>> GetAllServiceHistoriesAsync();
        Task<ServiceHistory> GetServiceHistoryByIdAsync(string id);
        Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory);
        Task<DeleteResult> DeleteServiceHistoryAsync(string id);
    }
}
