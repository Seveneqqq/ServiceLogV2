using ServiceLog.Filters;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.DeviceRepository
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetDevicesAsync(DeviceFilter filter);
        Task<Device?> GetDeviceByIdAsync(Guid id);
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device?> UpdateDeviceAsync(Guid id, Device device);
        Task DeleteDeviceAsync(Guid id);
    }
}
