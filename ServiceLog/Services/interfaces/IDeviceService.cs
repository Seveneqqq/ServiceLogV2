using ServiceLog.Filters;
using ServiceLog.Models.Dto.DeviceDto;

namespace ServiceLog.Services.interfaces
{
    public interface IDeviceService
    {
        Task<GetAllDeviceResponseDto> GetAllDevicesAsync(DeviceFilter? deviceFilter);
        Task<GetByIdDeviceResponseDto> GetDeviceByIdAsync(string id);
        Task<NewDeviceResponseDto> CreateDeviceAsync(NewDeviceRequestDto newDeviceRequestDto);
        Task<UpdateDeviceResponseDto> UpdateDeviceAsync(string id, UpdateDeviceRequestDto updateDeviceRequestDto);
        Task<DeleteDeviceResponseDto> DeleteDeviceAsync(string id);

    }
}
