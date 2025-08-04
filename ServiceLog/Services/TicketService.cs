using ServiceLog.Enums;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.DeviceDto;
using ServiceLog.Models.Dto.TicketDto;
using ServiceLog.Repositories.TicketRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.DeviceErrorCodes;
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
            if (ticketFilter == null)
            {
                return new GetAllTicketsResponseDto
                {
                    Success = false,
                    Message = "Invalid filter data.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }
            try
            {
                var tickets = await _ticketRepository.GetAllTicketsAsync(ticketFilter);
                if (tickets == null || !tickets.Any())
                {
                    return new GetAllTicketsResponseDto
                    {
                        Success = false,
                        Message = "No tickets found.",
                        ErrorCode = TicketErrorCode.TicketNotFound
                    };
                }
                return new GetAllTicketsResponseDto
                {
                    Success = true,
                    Message = "Tickets retrieved successfully.",
                    Tickets = tickets
                };
            }
            catch (Exception ex)
            {
                return new GetAllTicketsResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while retrieving tickets: {ex.Message}",
                    ErrorCode = TicketErrorCode.Unknown
                };
            }
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
            if (string.IsNullOrWhiteSpace(id) || updateTicketRequestDto == null)
            {
                return new UpdateTicketResponseDto
                {
                    Success = false,
                    Message = "Invalid request data.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }

            if (string.IsNullOrEmpty(updateTicketRequestDto.Status))
            {
                return new UpdateTicketResponseDto
                {
                    Success = false,
                    Message = "Status cannot be empty.",
                    ErrorCode = TicketErrorCode.EmptyFields
                };
            }

            try
            {
                var existingTicket = await _ticketRepository.GetTicketByIdAsync(id);
                if (existingTicket == null)
                {
                    return new UpdateTicketResponseDto
                    {
                        Success = false,
                        Message = "Ticket not found.",
                        ErrorCode = TicketErrorCode.TicketNotFound
                    };
                }

                //Todo: Sprawdzanie czy technik i client istnieją

                existingTicket.ReceivedDate = updateTicketRequestDto.ReceivedDate;
                existingTicket.Status = updateTicketRequestDto.Status ?? existingTicket.Status;
                existingTicket.Description = updateTicketRequestDto.Description ?? existingTicket.Description;
                existingTicket.ClientId = updateTicketRequestDto.ClientId ?? existingTicket.ClientId;
                existingTicket.TechnicanId = updateTicketRequestDto.TechnicanId ?? existingTicket.TechnicanId;
                existingTicket.ReceivingMethod = updateTicketRequestDto.ReceivingMethod ?? existingTicket.ReceivingMethod;
                existingTicket.ReturnMethod = updateTicketRequestDto.ReturnMethod ?? existingTicket.ReturnMethod;

                await _ticketRepository.UpdateTicketAsync(id, existingTicket);

                return new UpdateTicketResponseDto
                {
                    Success = true,
                    Message = "Ticket updated successfully."
                };

            }
            catch (Exception ex)
            {
                return new UpdateTicketResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while updating the ticket: {ex.Message}",
                    ErrorCode = TicketErrorCode.Unknown
                };
            }
        }
    }
}
