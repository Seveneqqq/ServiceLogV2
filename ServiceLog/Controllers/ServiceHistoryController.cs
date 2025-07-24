using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("")]
        public async Task<IActionResult> GetAllServiceHistoriesAsync()
        {
            try
            {
                var result = await _serviceHistoryService.GetAllServiceHistoriesAsync();
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
