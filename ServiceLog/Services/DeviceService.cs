using ServiceLog.Models.Dto.DeviceDto;
using ServiceLog.Repositories.Device;
using ServiceLog.Services.interfaces;

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

        public Task<NewDeviceResponseDto> CreateDeviceAsync(NewDeviceRequestDto newDeviceRequestDto)
        {
            throw new NotImplementedException();
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
