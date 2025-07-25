using ServiceLog.Enums;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.DeviceDto;
using ServiceLog.Repositories.DeviceRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.DeviceErrorCodes;

namespace ServiceLog.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICategoryService _categoryService;
        public DeviceService(IDeviceRepository deviceRepository, ICategoryService categoryService)
        {
            _deviceRepository = deviceRepository;
            _categoryService = categoryService;
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
                    Message = $"An error occurred while creating the device: {ex.Message}",
                    ErrorCode = DeviceErrorCode.Unknown
                };
            }
        }

        public Task<DeleteDeviceResponseDto> DeleteDeviceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllDeviceResponseDto> GetAllDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIdDeviceResponseDto> GetDeviceByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateDeviceResponseDto> UpdateDeviceAsync(string id, UpdateDeviceRequestDto updateDeviceRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
