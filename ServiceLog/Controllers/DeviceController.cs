using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Filters;
using ServiceLog.Models.Dto.DeviceDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.DeviceErrorCodes;

namespace ServiceLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Create a new Device
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateNewDeviceAsync([FromBody] NewDeviceRequestDto newDeviceRequestDto)
        {
            try
            {
                var result = await _deviceService.CreateDeviceAsync(newDeviceRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    DeviceErrorCode.DeviceNotFound => NotFound(result),
                    DeviceErrorCode.InvalidData => Unauthorized(result),
                    DeviceErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Display an existing device by ID
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _deviceService.GetDeviceByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    DeviceErrorCode.DeviceNotFound => NotFound(result),
                    DeviceErrorCode.InvalidData => Unauthorized(result),
                    DeviceErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Delete an existing device by ID
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceAsync([FromRoute] string id)
        {
            try
            {
                var result = await _deviceService.DeleteDeviceAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    DeviceErrorCode.DeviceNotFound => NotFound(result),
                    DeviceErrorCode.InvalidData => Unauthorized(result),
                    DeviceErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Get all devices
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllDevicesAsync([FromQuery] DeviceFilter? deviceFilter)
        {
            try
            {
                var result = await _deviceService.GetAllDevicesAsync(deviceFilter);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    DeviceErrorCode.DeviceNotFound => NotFound(result),
                    DeviceErrorCode.InvalidData => Unauthorized(result),
                    DeviceErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Display service history by device ID
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("{id}/service-history")]
        public async Task<IActionResult> GetDeviceServiceHistoryAsync([FromRoute] string id)
        {
            try
            {
                var result = await _deviceService.getDeviceServiceHistory(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    DeviceErrorCode.DeviceNotFound => NotFound(result),
                    DeviceErrorCode.InvalidData => Unauthorized(result),
                    DeviceErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        //Todo: dodanie update dla device
    }
}
