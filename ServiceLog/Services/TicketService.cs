using ServiceLog.Filters;
using ServiceLog.Models.Dto.TicketDto;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Services
{
    public class TicketService : ITicketService
    {
        public async Task<CreateTicketResponseDto> CreateTicketAsync(CreateTicketRequestDto createTicketRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteTicketResponseDto> DeleteTicketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllTicketsResponseDto> GetAllTicketsAsync(TicketFilter ticketFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<GetTicketByIdResponseDto> GetTicketByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateTicketResponseDto> UpdateTicketAsync(string id, UpdateTicketRequestDto updateTicketRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
