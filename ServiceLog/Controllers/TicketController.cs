using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Filters;
using ServiceLog.Models.Dto.TicketDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.TicketErrorCodes;

namespace ServiceLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [Authorize(Roles = "Client, Technican, Admin")]
        [HttpPost("")]
        public async Task<IActionResult> CreateNewTicketAsync([FromBody] CreateTicketRequestDto createTicketRequestDto)
        {
            try
            {
                var result = await _ticketService.CreateTicketAsync(createTicketRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        [Authorize(Roles = "Client, Technican, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _ticketService.GetTicketByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        [Authorize(Roles = "Client, Technican, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketAsync([FromRoute] string id)
        {
            try
            {
                var result = await _ticketService.DeleteTicketAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        [Authorize(Roles = "Technican, Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllTicketsAsync([FromQuery] TicketFilter? ticketFilter)
        {
            try
            {
                var result = await _ticketService.GetAllTicketsAsync(ticketFilter);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        //Todo: Dodanie wyświetlania ticketów tylko danego użytkownika

        [Authorize(Roles = "Client, Technican, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicketAsync([FromRoute] string id,[FromBody] UpdateTicketRequestDto updateTicketRequestDto)
        {
            try
            {
                var result = await _ticketService.UpdateTicketAsync(id,updateTicketRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }
        [Authorize(Roles = "Technican, Admin")]
        [HttpPost("{id}/devices")]
        public async Task<IActionResult> AddDevicesToTicketAsync([FromRoute] string id, [FromBody] AddDevicesToTicketRequestDto addDevicesToTicketRequestDto)
        {
            try
            {
                var result = await _ticketService.AddDevicesToTicketAsync(id, addDevicesToTicketRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    TicketErrorCode.TicketNotFound => NotFound(result),
                    TicketErrorCode.InvalidData => Unauthorized(result),
                    TicketErrorCode.EmptyFields => BadRequest(result),
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
