using Microsoft.AspNetCore.Identity;
using ServiceLog.Data;
using ServiceLog.Models.Dto;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Services
{
    public class AuthService : IAuthService
    {

        public readonly UserManager<IdentityUser> _userManager;
        public readonly ITokenService _tokenService;

        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {

            IdentityUser user;

            if (string.IsNullOrWhiteSpace(loginDto.Username) && string.IsNullOrWhiteSpace(loginDto.Email))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Username or email cannot be empty.",
                    ErrorCode = AuthErrorCode.EmptyFields
                };
            }

            if (string.IsNullOrEmpty(loginDto.Password))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Password cannot be empty.",
                    ErrorCode = AuthErrorCode.EmptyFields
                };
            }

            if (!string.IsNullOrWhiteSpace(loginDto.Email))
            {
                user = await _userManager.FindByEmailAsync(loginDto.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDto.Username);
            }

            if (user != null)
            {

                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                var userRole = await _userManager.GetRolesAsync(user);

                return new LoginResponseDto
                {
                    Success = checkPasswordResult,
                    Message = checkPasswordResult ? "User logged successfully !" : "Incorrect password !",
                    Token = checkPasswordResult ? _tokenService.GenerateToken(user, string.Join(",", userRole)) : null,
                    Role = string.Join(",", userRole), 
                    ErrorCode = checkPasswordResult ? AuthErrorCode.None : AuthErrorCode.InvalidPassword
                };

            }

            return new LoginResponseDto
            {
                Success = false,
                Message = "User doesn't exists.",
                ErrorCode = AuthErrorCode.UserNotFound
            };

        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            if(string.IsNullOrWhiteSpace(registerDto.Username) && string.IsNullOrWhiteSpace(registerDto.Email))
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Username or email cannot be empty."
                };
            }

            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Passwords do not match."
                };
            }

            if(!string.IsNullOrWhiteSpace(user.Email) || !string.IsNullOrWhiteSpace(user.UserName)) { 
                var findByUsername = await _userManager.FindByNameAsync(user.UserName);
                var findByEmail = await _userManager.FindByEmailAsync(user.Email);

                if (findByUsername != null || findByEmail != null)
                {
                    return new RegisterResponseDto
                    {
                        Success = false,
                        Message = "A user with this username or email already exists."
                    };
                }
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                 await _userManager.AddToRoleAsync(user, registerDto.Role);
            }

            return new RegisterResponseDto
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "User registered successfully !" : string.Join("; ", result.Errors.Select(e => e.Description)),
                Token = result.Succeeded ? _tokenService.GenerateToken(user, registerDto.Role) : null,
                Role = registerDto.Role
            };

        }

    }
}
