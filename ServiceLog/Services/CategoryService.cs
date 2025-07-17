using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories.CategoryRepository;
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

            if(newCategoryRequestDto.ServiceOptions == null && newCategoryRequestDto.Name == null && newCategoryRequestDto.Description == null)
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = "Category request cannot be empty.",
                };
            }

            if (string.IsNullOrEmpty(newCategoryRequestDto.Name))
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = "Category name cannot be empty.",
                };
            }

            if(newCategoryRequestDto.ServiceOptions == null || !newCategoryRequestDto.ServiceOptions.Any())
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = "Service options cannot be empty.",
                };
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

            public async Task<DeleteCategoryResponseDto> DeleteCategoryAsync(string id)
            {
                throw new NotImplementedException();
            }

            public async Task<Category> UpdateCategoryAsync(string id, Category category)
            {
                throw new NotImplementedException();
            }

            public async Task<GetAllCategoryDto> GetAllCategoriesAsync()
            {
            try
            {
                List<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
                
                if(categories == null || !categories.Any())
                {
                    return new GetAllCategoryDto
                    {
                        Success = false,
                        Message = "No categories found."
                    };
                }
                else
                {
                    return new GetAllCategoryDto
                    {
                        Success = true,
                        Categories = categories,
                        Message = "Categories retrieved successfully."
                    };
                }
            }
            catch (Exception e)
            {
                return new GetAllCategoryDto
                {
                    Success = false,
                    Message = $"Error retrieving categories: {e.Message}",
                };
            }
        }

            public async Task<GetByIdCategoryResponseDto> GetCategoryByIdAsync(string id)
            {
                try
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(id);
                    if (category != null)
                    {
                        return new GetByIdCategoryResponseDto
                        {
                            Success = true,
                            Message = "Category retrieved successfully.",
                            Category = category

                        };
                    }
                    else
                    {
                        return new GetByIdCategoryResponseDto
                        {
                            Success = false,
                            Message = "Category not found."
                        };
                    
                    }
                }
                catch(Exception e)
                {
                    return new GetByIdCategoryResponseDto
                    {
                        Success = false,
                        Message = $"Error retrieving category: {e.Message}"
                    };
                }
            }

       }
}
