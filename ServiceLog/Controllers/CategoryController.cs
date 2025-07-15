using Microsoft.AspNetCore.Mvc;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Services.interfaces;

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
        public async Task<IActionResult> CreateNewCategory([FromBody] NewCategoryRequestDto newCategoryRequestDto)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }

        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync();
                if (result.Success)
                {
                    return Ok(result);
                }
                else if(result.Categories == null || !result.Categories.Any())
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500,$"Error:: {e.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCategories([FromRoute] string id)
        {
            try
            {
                var result = await _categoryService.GetCategoryByIdAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                else if (!result.Success && result.Category == null && !result.Message.Contains("Error"))
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:: {e.Message}");
            }

        }
    }
}
