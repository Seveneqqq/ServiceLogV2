using Microsoft.AspNetCore.Identity;
using ServiceLog.Models.Dto;

namespace ServiceLog.Services.interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        public Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);

        //reset hasla
    }
}
