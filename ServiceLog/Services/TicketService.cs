using ServiceLog.Enums;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.TicketDto;
using ServiceLog.Repositories.TicketRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.TicketErrorCodes;

namespace ServiceLog.Services
{

    //Todo: Utworzenie metody która będzie zmianiała status ticketu

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<CreateTicketResponseDto> CreateTicketAsync(CreateTicketRequestDto createTicketRequestDto)
        {
            if (createTicketRequestDto == null)
            {
                return new CreateTicketResponseDto
                {
                    Success = false,
                    Message = "Invalid request data.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }
            if (
                string.IsNullOrEmpty(createTicketRequestDto.ReceivedDate.ToString()) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.Status) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.Description) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.ClientId) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.TechnicanId) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.ReceivingMethod) ||
                string.IsNullOrWhiteSpace(createTicketRequestDto.ReturnMethod) 
                )
            {

                //Todo: Sprawdzanie czy Client i technican istnieją w bazie

                return new CreateTicketResponseDto
                {
                    Success = false,
                    Message = "All fields are required.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }
            try
            {
                var ticket = new Ticket
                {
                    ReceivedDate = createTicketRequestDto.ReceivedDate,
                    ResolvedDate = null, 
                    Status = createTicketRequestDto.Status,
                    Devices = null,
                    Description = createTicketRequestDto.Description,
                    ClientId = createTicketRequestDto.ClientId,
                    TechnicanId = createTicketRequestDto.TechnicanId,
                    ReceivingMethod = createTicketRequestDto.ReceivingMethod,
                    ReturnMethod = createTicketRequestDto.ReturnMethod
                };

                await _ticketRepository.CreateTicketAsync(ticket);

                return new CreateTicketResponseDto
                {
                    Success = true,
                    Message = "Ticket created successfully."
                };
            }
            catch (Exception ex)
            {
                return new CreateTicketResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while creating the ticket: {ex.Message}",
                    ErrorCode = TicketErrorCode.Unknown
                };
            }
        }

        public async Task<DeleteTicketResponseDto> DeleteTicketAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new DeleteTicketResponseDto
                {
                    Success = false,
                    Message = "Ticket ID cannot be null or empty.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return new DeleteTicketResponseDto
                    {
                        Success = false,
                        Message = "Ticket not found.",
                        ErrorCode = TicketErrorCode.TicketNotFound
                    };
                }
                await _ticketRepository.DeleteTicketAsync(ticket.Id);
                return new DeleteTicketResponseDto
                {
                    Success = true,
                    Message = "Ticket deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteTicketResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while deleting the ticket: {ex.Message}",
                    ErrorCode = TicketErrorCode.Unknown
                };
            }
        }

        public async Task<GetAllTicketsResponseDto> GetAllTicketsAsync(TicketFilter ticketFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<GetTicketByIdResponseDto> GetTicketByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new GetTicketByIdResponseDto
                {
                    Success = false,
                    Message = "Ticket ID cannot be null or empty.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return new GetTicketByIdResponseDto
                    {
                        Success = false,
                        Message = "Ticket not found.",
                        ErrorCode = TicketErrorCode.TicketNotFound
                    };
                }
                return new GetTicketByIdResponseDto
                {
                    Success = true,
                    Message = "Ticket retrieved successfully.",
                    Ticket = ticket
                };
            }
            catch (Exception ex)
            {
                return new GetTicketByIdResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the ticket: {ex.Message}",
                    ErrorCode = TicketErrorCode.Unknown
                };
            }
        }

        public async Task<UpdateTicketResponseDto> UpdateTicketAsync(string id, UpdateTicketRequestDto updateTicketRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
