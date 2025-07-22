using MongoDB.Driver;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Models.Dto.ServiceHistoryDto;
using ServiceLog.Repositories.ServiceHistoryRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.ServiceHistoryErrorCodes;

namespace ServiceLog.Services
{
    public class ServiceHistoryService : IServiceHistoryService
    {

        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public ServiceHistoryService(IServiceHistoryRepository serviceHistoryRepository)
        {
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        public async Task<CreateServiceHistoryResponseDto> CreateServiceHistoryAsync(CreateServiceHistoryRequestDto createServiceHistoryRequestDto)
        {
            try
            {
                if (createServiceHistoryRequestDto == null)
                {
                    return new CreateServiceHistoryResponseDto
                    {
                        Success = false,
                        Message = "Request data is null",
                        ErrorCode = ServiceHistoryErrorCode.EmptyFields
                    };
                }
                if (string.IsNullOrEmpty(createServiceHistoryRequestDto.IssueDescription) ||
                    string.IsNullOrEmpty(createServiceHistoryRequestDto.TicketId) ||
                    string.IsNullOrEmpty(createServiceHistoryRequestDto.TechnicanId) ||
                    string.IsNullOrEmpty(createServiceHistoryRequestDto.DeviceId) ||
                    createServiceHistoryRequestDto.PerformedServiceOptions == null
                    )
                {
                    return new CreateServiceHistoryResponseDto
                    {
                        Success = false,
                        Message = "Required fields are missing",
                        ErrorCode = ServiceHistoryErrorCode.EmptyFields
                    };
                }

                ServiceHistory serviceHistory = new ServiceHistory
                {
                    IssueDescription = createServiceHistoryRequestDto.IssueDescription,
                    OtherInformations = createServiceHistoryRequestDto.OtherInformations,
                    TicketId = createServiceHistoryRequestDto.TicketId,
                    TechnicanId = createServiceHistoryRequestDto.TechnicanId,
                    DeviceId = createServiceHistoryRequestDto.DeviceId,
                    PerformedServiceOptions = createServiceHistoryRequestDto.PerformedServiceOptions
                };

                await _serviceHistoryRepository.CreateServiceHistoryAsync(serviceHistory);

                return new CreateServiceHistoryResponseDto
                {
                    Success = true,
                    Message = "Service history created successfully.",
                };

            }
            catch (Exception e) {
                return new CreateServiceHistoryResponseDto
                {
                    Success = false,
                    Message = $"Error creating service history: {e.Message}",
                    ErrorCode = ServiceHistoryErrorCode.Unknown
                };
            }
        }

        public async Task<ServiceHistoryResponseDto> DeleteServiceHistoryAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new ServiceHistoryResponseDto
                {
                    Success = false,
                    Message = "Service history ID cannot be null or empty.",
                    ErrorCode = ServiceHistoryErrorCode.EmptyFields
                };
            }
            try
            {
                var serviceHistory = await _serviceHistoryRepository.GetServiceHistoryByIdAsync(id);
                if (serviceHistory == null)
                {
                    return new ServiceHistoryResponseDto
                    {
                        Success = false,
                        Message = "Service history not found.",
                        ErrorCode = ServiceHistoryErrorCode.ServiceHistoryNotFound
                    };
                }
                await _serviceHistoryRepository.DeleteServiceHistoryAsync(id);
                return new ServiceHistoryResponseDto
                {
                    Success = true,
                    Message = "Service history deleted successfully."
                };
            }
            catch (Exception e)
            {
                return new ServiceHistoryResponseDto
                {
                    Success = false,
                    Message = $"Error deleting service history: {e.Message}",
                    ErrorCode = ServiceHistoryErrorCode.Unknown
                };
            }
        }

        public async Task<GetAllServiceHistoriesResposneDto> GetAllServiceHistoriesAsync()
        {
            try
            {
                var serviceHistories = await _serviceHistoryRepository.GetAllServiceHistoriesAsync();
                if (serviceHistories == null || !serviceHistories.Any())
                {
                    return new GetAllServiceHistoriesResposneDto
                    {
                        Success = false,
                        Message = "No service histories found.",
                        ErrorCode = ServiceHistoryErrorCode.ServiceHistoryNotFound
                    };
                }
                return new GetAllServiceHistoriesResposneDto
                {
                    Success = true,
                    Message = "Service histories retrieved successfully.",
                    ServiceHistories = serviceHistories
                };

            }
            catch (Exception e)
            {
                return new GetAllServiceHistoriesResposneDto
                {
                    Success = false,
                    Message = $"Error retrieving service histories: {e.Message}",
                    ErrorCode = ServiceHistoryErrorCode.Unknown
                };
            }
        }

        public async Task<GetByIdServiceHistoryResponseDto> GetServiceHistoryByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new GetByIdServiceHistoryResponseDto
                {
                    Success = false,
                    Message = "Service history ID cannot be null or empty.",
                    ErrorCode = ServiceHistoryErrorCode.EmptyFields
                };
            }
            try
            {
                var serviceHistory = await _serviceHistoryRepository.GetServiceHistoryByIdAsync(id);
                if (serviceHistory == null)
                {
                    return new GetByIdServiceHistoryResponseDto
                    {
                        Success = false,
                        Message = "Service history not found.",
                        ErrorCode = ServiceHistoryErrorCode.ServiceHistoryNotFound
                    };
                }
                return new GetByIdServiceHistoryResponseDto
                {
                    Success = true,
                    Message = "Service history retrieved successfully.",
                    ServiceHistory = serviceHistory
                };
            }
            catch (Exception e)
            {
                return new GetByIdServiceHistoryResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving service history: {e.Message}",
                    ErrorCode = ServiceHistoryErrorCode.Unknown
                };
            }
        }

        public async Task<ServiceHistoryResponseDto> UpdateServiceHistoryAsync(string id, UpdateServiceHistoryRequestDto updateServiceHistoryRequestDto)
        {
            if (updateServiceHistoryRequestDto == null)
            {
                return new ServiceHistoryResponseDto
                {
                    Success = false,
                    Message = "Request data is null.",
                    ErrorCode = ServiceHistoryErrorCode.EmptyFields
                };
            }

            if (!string.IsNullOrEmpty(id)) {
                return new ServiceHistoryResponseDto
                {
                    Success = false,
                    Message = "Service history ID cannot be null or empty.",
                    ErrorCode = ServiceHistoryErrorCode.EmptyFields
                };
            }

            try
            {
                var serviceHistory = await _serviceHistoryRepository.GetServiceHistoryByIdAsync(id);
                if (serviceHistory == null)
                {
                    return new ServiceHistoryResponseDto
                    {
                        Success = false,
                        Message = "Service history not found.",
                        ErrorCode = ServiceHistoryErrorCode.ServiceHistoryNotFound
                    };
                }

                serviceHistory.IssueDescription = updateServiceHistoryRequestDto.IssueDescription ?? serviceHistory.IssueDescription;
                serviceHistory.OtherInformations = updateServiceHistoryRequestDto.OtherInformations ?? serviceHistory.OtherInformations;
                serviceHistory.TicketId = updateServiceHistoryRequestDto.TicketId ?? serviceHistory.TicketId;
                serviceHistory.TechnicanId = updateServiceHistoryRequestDto.TechnicanId ?? serviceHistory.TechnicanId;
                serviceHistory.DeviceId = updateServiceHistoryRequestDto.DeviceId ?? serviceHistory.DeviceId;
                serviceHistory.PerformedServiceOptions = updateServiceHistoryRequestDto.PerformedServiceOptions ?? serviceHistory.PerformedServiceOptions;
                
                await _serviceHistoryRepository.UpdateServiceHistoryAsync(id, serviceHistory);
                
                return new ServiceHistoryResponseDto
                {
                    Success = true,
                    Message = "Service history updated successfully."
                };
            }
            catch(Exception e)
            {
                return new ServiceHistoryResponseDto
                {
                    Success = false,
                    Message = $"Error updating service history: {e.Message}",
                    ErrorCode = ServiceHistoryErrorCode.Unknown
                };
            }
        }
    }
}
