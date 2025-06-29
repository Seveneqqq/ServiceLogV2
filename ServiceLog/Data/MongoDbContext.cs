using MongoDB.Driver;
using ServiceLog.Models.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLog.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            try
            {
                Console.WriteLine($"MongoDB ConnectionString: {settings.Value.ConnectionString}");
                Console.WriteLine($"MongoDB DatabaseName: {settings.Value.DatabaseName}");
 
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.DatabaseName);
        
                Console.WriteLine("MongoDB connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw;
            }
        }

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Category");
        public IMongoCollection<Service_history> ServiceHistories => _database.GetCollection<Service_history>("service_histories");
        public IMongoCollection<Ticket> Tickets => _database.GetCollection<Ticket>("tickets");
    }
}