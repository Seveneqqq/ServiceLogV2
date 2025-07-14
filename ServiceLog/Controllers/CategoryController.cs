using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Models.Dto.AuthDto;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories;
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
                return BadRequest($"Error:: {e.Message}");
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
                return BadRequest($"Error:: {e.Message}");
            }

        }
    }
}
