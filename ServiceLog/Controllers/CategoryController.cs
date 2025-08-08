using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.CategoryErrorCodes;

namespace ServiceLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateNewCategoryAsync([FromBody] NewCategoryRequestDto newCategoryRequestDto)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    CategoryErrorCode.CategoryNotFound => NotFound(result),
                    CategoryErrorCode.InvalidData => Unauthorized(result),
                    CategoryErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync();
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    CategoryErrorCode.CategoryNotFound => NotFound(result),
                    CategoryErrorCode.InvalidData => Unauthorized(result),
                    CategoryErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };

            }
            catch (Exception e)
            {
                return StatusCode(500,$"Error:: {e.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _categoryService.GetCategoryByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    CategoryErrorCode.CategoryNotFound => NotFound(result),
                    CategoryErrorCode.InvalidData => Unauthorized(result),
                    CategoryErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
        {
            try
            { 
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    CategoryErrorCode.CategoryNotFound => NotFound(result),
                    CategoryErrorCode.InvalidData => Unauthorized(result),
                    CategoryErrorCode.EmptyFields => BadRequest(result),
                    _ => BadRequest(result)
                };

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, updateCategoryRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return result.ErrorCode switch
                {
                    CategoryErrorCode.CategoryNotFound => NotFound(result),
                    CategoryErrorCode.InvalidData => Unauthorized(result),
                    CategoryErrorCode.EmptyFields => BadRequest(result),
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
 