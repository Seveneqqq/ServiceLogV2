using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServiceLog.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var clientRoleId = "1e9391b3-5114-43f6-80a5-ecaa658fdfc1";
            var technicanRoleId = "939ae2c6-762d-4290-b3e8-53651ed70b29";
            var adminRoleId = "b9641eac-4dad-4fc7-8db0-3c969e0d9a90";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = clientRoleId,
                    ConcurrencyStamp = clientRoleId,
                    Name = "Client",
                    NormalizedName = "Client".ToUpper()
                },
                new IdentityRole
                {
                    Id = technicanRoleId,
                    ConcurrencyStamp = technicanRoleId,
                    Name = "Technican",
                    NormalizedName = "Technican".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
