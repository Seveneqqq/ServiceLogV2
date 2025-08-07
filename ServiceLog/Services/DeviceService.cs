using ServiceLog.Enums;
using ServiceLog.Filters;
using ServiceLog.Helpers;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.DeviceDto;
using ServiceLog.Repositories.DeviceRepository;
using ServiceLog.Repositories.ServiceHistoryRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.DeviceErrorCodes;

namespace ServiceLog.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICategoryService _categoryService;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public DeviceService(IDeviceRepository deviceRepository, ICategoryService categoryService, IServiceHistoryRepository serviceHistoryRepository)
        {
            _deviceRepository = deviceRepository;
            _categoryService = categoryService;
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        private async Task<bool> IsCategoryValidAsync(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                return false;
            }
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            return category != null && category.Success;
        }
     
        public async Task<NewDeviceResponseDto> CreateDeviceAsync(NewDeviceRequestDto newDeviceRequestDto)
        {
            if(newDeviceRequestDto == null)
            {
                return new NewDeviceResponseDto
                {
                    Success = false,
                    Message = "Device request cannot be null.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
           
            if(string.IsNullOrEmpty(newDeviceRequestDto.SerialNumber) || string.IsNullOrEmpty(newDeviceRequestDto.Designation) || string.IsNullOrEmpty(newDeviceRequestDto.CategoryId) || string.IsNullOrEmpty(newDeviceRequestDto.Status))
            {
                return new NewDeviceResponseDto
                {
                    Success = false,
                    Message = "All fields are required.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
           
            if (!await IsCategoryValidAsync(newDeviceRequestDto.CategoryId))
            {
                return new NewDeviceResponseDto
                {
                    Success = false,
                    Message = "Invalid category ID.",
                    ErrorCode = DeviceErrorCode.InvalidData
                };
            }

            try
            {
                Device device = new Device { 
                    Status = newDeviceRequestDto.Status,
                    Short_id = ShortCodeGenerator.GenerateShortId(),
                    SerialNumber = newDeviceRequestDto.SerialNumber, 
                    Designation = newDeviceRequestDto.Designation, 
                    Location = newDeviceRequestDto.Location, 
                    CategoryId = newDeviceRequestDto.CategoryId 
                };

                var result = await _deviceRepository.CreateDeviceAsync(device);
                if (result == null)
                {
                    return new NewDeviceResponseDto
                    {
                        Success = false,
                        Message = "Failed to create device.",
                        ErrorCode = DeviceErrorCode.Unknown
                    };
                }
                else
                {
                    return new NewDeviceResponseDto
                    {
                        Success = true,
                        Message = $"Device created successfully with ID: {result.Id}.",
                        Device = result,
                    };
                }
            }
            catch (Exception ex)
            {
                return new NewDeviceResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while creating the device: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }

        public async Task<GetDeviceServiceHistoryDto> getDeviceServiceHistory(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new GetDeviceServiceHistoryDto
                {
                    Success = false,
                    Message = "Device ID cannot be null or empty.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return new GetDeviceServiceHistoryDto
                {
                    Success = false,
                    Message = "Invalid Device ID format.",
                    ErrorCode = DeviceErrorCode.InvalidData
                };
            }
            try
            {
                var device = await _deviceRepository.GetDeviceByIdAsync(guidId);
                if (device == null)
                {
                    return new GetDeviceServiceHistoryDto
                    {
                        Success = false,
                        Message = "Device not found.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }

                ServiceHistoryFilter serviceHistoryFilter = new ServiceHistoryFilter
                {
                    DeviceId = id
                };

                var serviceHistoryResult = await _serviceHistoryRepository.GetAllServiceHistoriesAsync(serviceHistoryFilter);
                if (serviceHistoryResult == null)
                {
                    return new GetDeviceServiceHistoryDto
                    {
                        Success = false,
                        Message = "No service history found for this device.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }
                return new GetDeviceServiceHistoryDto
                {
                    Success = true,
                    Message = "Service history retrieved successfully.",
                    serviceHistories = serviceHistoryResult
                };
            }
            catch (Exception ex)
            {
                return new GetDeviceServiceHistoryDto
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the service history: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }

        public async Task<DeleteDeviceResponseDto> DeleteDeviceAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new DeleteDeviceResponseDto
                {
                    Success = false,
                    Message = "Device ID cannot be null or empty.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }

            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                {
                    return new DeleteDeviceResponseDto
                    {
                        Success = false,
                        Message = "Invalid Device ID format.",
                        ErrorCode = DeviceErrorCode.InvalidData
                    };
                }

                var device = await _deviceRepository.GetDeviceByIdAsync(guidId);

                if (device == null)
                {
                    return new DeleteDeviceResponseDto
                    {
                        Success = false,
                        Message = "Device not found.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }

                await _deviceRepository.DeleteDeviceAsync(device.Id);

                    return new DeleteDeviceResponseDto
                    {
                        Success = true,
                        Message = "Device deleted successfully."
                    };
            }
            catch (Exception ex)
            {
                return new DeleteDeviceResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while deleting the device: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }

        }

        public async Task<GetAllDeviceResponseDto> GetAllDevicesAsync(DeviceFilter? deviceFilter)
        {
            try
            {
                if (deviceFilter == null)
                {

                    var allResult = await _deviceRepository.GetDevicesAsync(null);

                    return new GetAllDeviceResponseDto
                    {
                        Success = true,
                        Message = "No filter applied, returning all devices.",
                        Devices = allResult
                    };
                }

                var filteredResult = await _deviceRepository.GetDevicesAsync(deviceFilter);

                if (filteredResult == null || filteredResult.Count == 0)
                {
                    return new GetAllDeviceResponseDto
                    {
                        Success = false,
                        Message = "No devices found.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }
                return new GetAllDeviceResponseDto
                {
                    Success = true,
                    Message = "Devices retrieved successfully.",
                    Devices = filteredResult
                };
            }
            catch (Exception ex)
            {
                return new GetAllDeviceResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while retrieving devices: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }

        public async Task<GetByIdDeviceResponseDto> GetDeviceByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new GetByIdDeviceResponseDto
                {
                    Success = false,
                    Message = "Device ID cannot be null or empty.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return new GetByIdDeviceResponseDto
                {
                    Success = false,
                    Message = "Invalid Device ID format.",
                    ErrorCode = DeviceErrorCode.InvalidData
                };
            }
            try
            {
                var device = await _deviceRepository.GetDeviceByIdAsync(guidId);
                if (device == null)
                {
                    return new GetByIdDeviceResponseDto
                    {
                        Success = false,
                        Message = "Device not found.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }
                return new GetByIdDeviceResponseDto
                {
                    Success = true,
                    Message = "Device retrieved successfully.",
                    Device = device
                };
            }
            catch (Exception ex)
            {
                return new GetByIdDeviceResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the device: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }

        public async Task<UpdateDeviceResponseDto> UpdateDeviceAsync(string id, UpdateDeviceRequestDto updateDeviceRequestDto)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new UpdateDeviceResponseDto
                {
                    Success = false,
                    Message = "Device ID cannot be null or empty.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
            if (updateDeviceRequestDto == null)
            {
                return new UpdateDeviceResponseDto
                {
                    Success = false,
                    Message = "Update request cannot be null.",
                    ErrorCode = DeviceErrorCode.EmptyFields
                };
            }
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return new UpdateDeviceResponseDto
                {
                    Success = false,
                    Message = "Invalid Device ID format.",
                    ErrorCode = DeviceErrorCode.InvalidData
                };
            }
            try
            {
                var device = await _deviceRepository.GetDeviceByIdAsync(guidId);
                if (device == null)
                {
                    return new UpdateDeviceResponseDto
                    {
                        Success = false,
                        Message = "Device not found.",
                        ErrorCode = DeviceErrorCode.DeviceNotFound
                    };
                }
                
                var CategoryResult = await _categoryService.GetCategoryByIdAsync(updateDeviceRequestDto.Device.CategoryId);

                if (!CategoryResult.Success)
                {
                    return new UpdateDeviceResponseDto
                    {
                        Success = false,
                        Message = "Invalid category ID.",
                        ErrorCode = DeviceErrorCode.InvalidData
                    };
                }

                Device requestDevice = new Device
                {
                    Id = guidId,
                    SerialNumber = updateDeviceRequestDto.Device.SerialNumber,
                    Designation = updateDeviceRequestDto.Device.Designation,
                    Location = updateDeviceRequestDto.Device.Location,
                    CategoryId = updateDeviceRequestDto.Device.CategoryId,
                    Status = updateDeviceRequestDto.Device.Status
                };

                var updatedDevice = await _deviceRepository.UpdateDeviceAsync(guidId, device);

                if(updatedDevice == null)
                {
                    return new UpdateDeviceResponseDto
                    {
                        Success = false,
                        Message = "Failed to update device.",
                        ErrorCode = DeviceErrorCode.Unknown
                    };
                }

                return new UpdateDeviceResponseDto
                {
                    Success = true,
                    Message = "Device updated successfully.",
                    Device = updatedDevice
                };
            }
            catch (Exception ex)
            {
                return new UpdateDeviceResponseDto
                {
                    Success = false,
                    Message = $"An error occurred while updating the device: {ex.Message} {ex.InnerException}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }
    }
}
