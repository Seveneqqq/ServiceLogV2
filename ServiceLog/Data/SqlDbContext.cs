using Microsoft.EntityFrameworkCore;
using ServiceLog.Models.Domain;

namespace ServiceLog.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Device> Devices { get; set; }

    }
}
