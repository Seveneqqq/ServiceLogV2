﻿using Microsoft.AspNetCore.Mvc;
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

        //Todo: Dodanie /api/Device/123/service-history, czyli wstrzykniecie do serwisu device serwisu servicehistory i pobranie wszystkich rekordow uzywajac filtra z id urzadzenia

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
    }
}
