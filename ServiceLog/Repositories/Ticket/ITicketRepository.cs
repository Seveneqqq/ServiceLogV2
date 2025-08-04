using ServiceLog.Filters;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.TicketDto;

namespace ServiceLog.Repositories.TicketRepository
{
    public interface ITicketRepository
    {
        Task CreateTicketAsync(Ticket ticket);
        Task<List<Ticket>> GetAllTicketsAsync(TicketFilter? ticketFilter);
        Task<Ticket> GetTicketByIdAsync(string id);
        Task UpdateTicketAsync(string id, Ticket ticket);
        Task DeleteTicketAsync(string id);
    }
}
