using Microsoft.EntityFrameworkCore;
using ServiceLog.Data;
using ServiceLog.Filters;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.DeviceRepository
{
    public class DeviceRepository : IDeviceRepository
    {

        private readonly SqlDbContext _sqlDbContext;

        public DeviceRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<Device> CreateDeviceAsync(Device device)
        {
            _sqlDbContext.Devices.Add(device);
            await _sqlDbContext.SaveChangesAsync();
            return device;
        }

        public async Task DeleteDeviceAsync(Guid id)
        {
            _sqlDbContext.Devices.Remove(new Device { Id = id });
            await _sqlDbContext.SaveChangesAsync();
        }

        public async Task<Device?> GetDeviceByIdAsync(Guid id)
        {
            return await _sqlDbContext.Devices
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Device>> GetDevicesAsync(DeviceFilter filter)
        {
            return await _sqlDbContext.Devices.AsNoTracking().ToListAsync();
        }

        public async Task<Device?> UpdateDeviceAsync(Guid id, Device device)
        {
            device.Id = id;
            _sqlDbContext.Devices.Update(device);
            await _sqlDbContext.SaveChangesAsync();
            return device;
        }
    }
}
