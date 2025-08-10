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

        /// <summary>
        /// Display all users
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserFilter userFilter)
        {
            try
            {
                var result = await _userService.GetAllUsersAsync(userFilter);
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

        /// <summary>
        /// Get an existing user by ID
        /// </summary>
        [Authorize(Roles = "Client, Technican, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDataByIdAsync([FromRoute] string id)
        {
            try
            {

                //Todo: Użytkownik może wyświetlić tylko siebie

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

        /// <summary>
        /// Delete an existing user by ID
        /// </summary>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Update an existing user by ID
        /// </summary>
        [Authorize(Roles = "Admin")]
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
