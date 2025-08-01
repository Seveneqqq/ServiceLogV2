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

        public async Task<List<Device>> GetDevicesAsync(DeviceFilter? deviceFilter)
        {
            if(deviceFilter == null)
            {
                return await _sqlDbContext.Devices.ToListAsync();
            }
            var query = _sqlDbContext.Devices.AsQueryable();

            if (!string.IsNullOrEmpty(deviceFilter.ShortId))
            {
                query = query.Where(d => d.Short_id.Contains(deviceFilter.ShortId));
            }
            if (!string.IsNullOrEmpty(deviceFilter.SerialNumber))
            {
                query = query.Where(d => d.SerialNumber.Contains(deviceFilter.SerialNumber));
            }
            if (!string.IsNullOrEmpty(deviceFilter.Designation))
            {
                query = query.Where(d => d.Designation.Contains(deviceFilter.Designation));
            }
            if (!string.IsNullOrEmpty(deviceFilter.Location))
            {
                query = query.Where(d => d.Location.Contains(deviceFilter.Location));
            }
            if (!string.IsNullOrEmpty(deviceFilter.CategoryId))
            {
                query = query.Where(d => d.CategoryId == deviceFilter.CategoryId);
            }
            if (!string.IsNullOrEmpty(deviceFilter.Status))
            {
                query = query.Where(d => d.Status == deviceFilter.Status);
            }

            return await query.ToListAsync();

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
