using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Filters;
using ServiceLog.Models.Dto.ServiceHistoryDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.ServiceHistoryErrorCodes;

namespace ServiceLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly IServiceHistoryService _serviceHistoryService;
        public ServiceHistoryController(IServiceHistoryService serviceHistoryService)
        {
            _serviceHistoryService = serviceHistoryService;
        }

        /// <summary>
        /// Create a new service history
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateNewServiceHistoryAsync([FromBody] CreateServiceHistoryRequestDto createServiceHistoryRequestDto)
        {
            try
            {
                var result = await _serviceHistoryService.CreateServiceHistoryAsync(createServiceHistoryRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    ServiceHistoryErrorCode.InvalidData => BadRequest(result),
                    ServiceHistoryErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }   
        }

        /// <summary>
        /// Get all service histories
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllServiceHistoriesAsync([FromQuery] ServiceHistoryFilter serviceHistoryFilter)
        {
            try
            {
                var result = await _serviceHistoryService.GetAllServiceHistoriesAsync(serviceHistoryFilter);
                if (result.Success)
                {
                    return Ok(result);
                }

                return result.ErrorCode switch
                {
                    ServiceHistoryErrorCode.ServiceHistoryNotFound => NotFound(result),
                    _ => BadRequest(result)
                };

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Display an existing service history by ID
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceHistoryByIdAsync(string id)
        {
            try
            {
                var result = await _serviceHistoryService.GetServiceHistoryByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    ServiceHistoryErrorCode.ServiceHistoryNotFound => NotFound(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Delete an existing service history by ID
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceHistoryAsync(string id)
        {
            try
            {
                var result = await _serviceHistoryService.DeleteServiceHistoryAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    ServiceHistoryErrorCode.ServiceHistoryNotFound => NotFound(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        /// <summary>
        /// Update an existing service history
        /// </summary>
        [Authorize(Roles = "Technican, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceHistoryAsync(string id, [FromBody] UpdateServiceHistoryRequestDto updateServiceHistoryRequestDto)
        {
            try
            {
                var result = await _serviceHistoryService.UpdateServiceHistoryAsync(id, updateServiceHistoryRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    ServiceHistoryErrorCode.ServiceHistoryNotFound => NotFound(result),
                    ServiceHistoryErrorCode.InvalidData => BadRequest(result),
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
