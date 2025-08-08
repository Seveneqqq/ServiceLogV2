using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Filters;
using ServiceLog.Models.Dto.TicketDto;
using ServiceLog.Models.Dto.UserDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.TicketErrorCodes;
using static ServiceLog.Enums.UserErrorCodes;

namespace ServiceLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    UserErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDataByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _userService.GetUserDataByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    UserErrorCode.UserNotFound => NotFound(result),
                    UserErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _userService.DeleteUserByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    UserErrorCode.UserNotFound => NotFound(result),
                    UserErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserByIdAsync([FromRoute] string id, [FromBody] UpdateUserByIdRequestDto updateUserByIdRequestDto)
        {
            try
            {
                var result = await _userService.UpdateUserByIdAsync(id, updateUserByIdRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    UserErrorCode.UserNotFound => NotFound(result),
                    UserErrorCode.InvalidData => BadRequest(result),
                    UserErrorCode.EmptyFields => BadRequest(result),
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
