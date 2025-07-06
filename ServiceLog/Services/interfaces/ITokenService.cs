using Microsoft.AspNetCore.Identity;

namespace ServiceLog.Services.interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(IdentityUser user, string role);
        public bool ValidateToken(string token);
        
    }
}
