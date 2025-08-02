using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Models.Dto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.AuthErrorCodes;

namespace ServiceLog.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        //Todo: Dodać tabelę która będzie przechowywać technicanId i informacje o techniku
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                if (result.Success && !string.IsNullOrEmpty(result.Token))
                {
                    Response.Cookies.Append("jwt_token", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                if (result.Success && result.Token != null)
                {
                    Response.Cookies.Append("jwt_token", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                    return Ok(result);
                }

                return result.ErrorCode switch
                {
                    AuthErrorCode.UserNotFound => NotFound(result),
                    AuthErrorCode.InvalidPassword => Unauthorized(result),
                    AuthErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
    }
}
