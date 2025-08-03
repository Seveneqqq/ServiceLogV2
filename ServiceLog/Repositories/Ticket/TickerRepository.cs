using ServiceLog.Data;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.TicketDto;

namespace ServiceLog.Repositories.TicketRepository
{
    public class TickerRepository : ITicketRepository
    {
        private readonly MongoDbContext _mongoDbContext;
        public TickerRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        public Task CreateTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTicketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task GetAllTicketsAsync(TicketFilter? ticketFilter)
        {
            throw new NotImplementedException();
        }

        public Task GetAllTicketsByDeviceIdAsync(string deviceId)
        {
            throw new NotImplementedException();
        }

        public Task GetTicketByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicketAsync(string id, Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
