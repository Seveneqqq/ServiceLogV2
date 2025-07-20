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

        public async Task<DeleteResult> DeleteServiceHistoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ServiceHistory>> GetAllServiceHistoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceHistory> GetServiceHistoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ReplaceOneResult> UpdateServiceHistoryAsync(string id, ServiceHistory serviceHistory)
        {
            throw new NotImplementedException();
        }
    }
}
