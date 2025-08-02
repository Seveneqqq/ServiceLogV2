using ServiceLog.Filters;
using ServiceLog.Models.Dto.TicketDto;

namespace ServiceLog.Services.interfaces
{
    public interface ITicketService
    {
        Task<CreateTicketResponseDto> CreateTicketAsync(CreateTicketRequestDto createTicketRequestDto);
        Task<GetTicketByIdResponseDto> GetTicketByIdAsync(string id);
        Task<GetAllTicketsResponseDto> GetAllTicketsAsync(TicketFilter ticketFilter);
        Task<UpdateTicketResponseDto> UpdateTicketAsync(string id, UpdateTicketRequestDto updateTicketRequestDto);
        Task<DeleteTicketResponseDto> DeleteTicketAsync(string id);
    }
}
