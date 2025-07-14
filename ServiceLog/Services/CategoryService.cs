using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<NewCategoryResponseDto> CreateCategoryAsync(NewCategoryRequestDto newCategoryRequestDto)
            {

                if(newCategoryRequestDto == null || string.IsNullOrEmpty(newCategoryRequestDto.Name))
            {
                throw new ArgumentException("Invalid category data.");
            }

            try
            {
                Category category = new Category
                {
                    Name = newCategoryRequestDto.Name,
                    Description = newCategoryRequestDto.Description,
                    ServiceOptions = newCategoryRequestDto.ServiceOptions,
                };

                await _categoryRepository.CreateCategoryAsync(category);

                return new NewCategoryResponseDto
                {
                    Success = true,
                    Message = "Category created successfully.",
                };
            }
            catch (Exception e)
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = $"Error creating category: {e.Message}",
                };
            }
        }

            public async Task DeleteCategoryAsync(string id)
            {
                throw new NotImplementedException();
            }

            public async Task<List<Category>> GetAllCategoriesAsync()
            {
                throw new NotImplementedException();
            }

            public async Task<Category> GetCategoryByIdAsync(string id)
            {
                throw new NotImplementedException();
            }

            public async Task<Category> UpdateCategoryAsync(string id, Category category)
            {
                throw new NotImplementedException();
            }
       }
}
