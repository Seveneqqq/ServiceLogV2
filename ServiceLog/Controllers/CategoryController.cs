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
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error:: {e.Message}");
            }

        }
    }
}
