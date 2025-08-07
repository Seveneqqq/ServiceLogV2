using MongoDB.Driver;
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
        public async Task CreateTicketAsync(Ticket ticket)
        {
           await _mongoDbContext.Tickets.InsertOneAsync(ticket);
        }

        public async Task DeleteTicketAsync(string id)
        {
            await _mongoDbContext.Tickets.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(TicketFilter? ticketFilter)
        {
            if (ticketFilter == null)
            {
                return await _mongoDbContext.Tickets.Find(_ => true).ToListAsync();
            }
            var filterBuilder = Builders<Ticket>.Filter;
            var filter = filterBuilder.Empty;
            if (!string.IsNullOrEmpty(ticketFilter.Description))
            {
                filter &= filterBuilder.Eq(t => t.Description, ticketFilter.Description);
            }
            if (!string.IsNullOrEmpty(ticketFilter.ReturnMethod))
            {
                filter &= filterBuilder.Eq(t => t.ReturnMethod, ticketFilter.ReturnMethod);
            }
            if (!string.IsNullOrEmpty(ticketFilter.Status))
            {
                filter &= filterBuilder.Eq(t => t.Status, ticketFilter.Status);
            }
            if (!string.IsNullOrEmpty(ticketFilter.ClientId))
            {
                filter &= filterBuilder.Eq(t => t.ClientId, ticketFilter.ClientId);
            }
            if (!string.IsNullOrEmpty(ticketFilter.TechnicanId))
            {
                filter &= filterBuilder.Eq(t => t.TechnicanId, ticketFilter.TechnicanId);
            }
            if (ticketFilter.ReceivedDate.HasValue)
            {
                filter &= filterBuilder.Gte(t => t.ReceivedDate, ticketFilter.ReceivedDate.Value);
            }
            if (ticketFilter.ResolvedDate.HasValue)
            {
                filter &= filterBuilder.Lte(t => t.ResolvedDate, ticketFilter.ResolvedDate.Value);
            }
            return await _mongoDbContext.Tickets.Find(filter).ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(string id)
        {
            return await _mongoDbContext.Tickets.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateTicketAsync(string id, Ticket ticket)
        {
            await _mongoDbContext.Tickets.ReplaceOneAsync(x => x.Id == id, ticket);
        }
    }
}
