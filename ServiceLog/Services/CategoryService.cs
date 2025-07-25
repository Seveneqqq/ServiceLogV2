﻿using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories.CategoryRepository;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.CategoryErrorCodes;

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
                    ErrorCode = CategoryErrorCode.EmptyFields
                };
            }

            if (string.IsNullOrEmpty(newCategoryRequestDto.Name))
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = "Category name cannot be empty.",
                    ErrorCode = CategoryErrorCode.EmptyFields
                };
            }

            if(newCategoryRequestDto.ServiceOptions == null || !newCategoryRequestDto.ServiceOptions.Any())
            {
                return new NewCategoryResponseDto
                {
                    Success = false,
                    Message = "Service options cannot be empty.",
                    ErrorCode = CategoryErrorCode.EmptyFields
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
                    ErrorCode = CategoryErrorCode.Unknown
                };
            }
        }

            public async Task<DeleteCategoryResponseDto> DeleteCategoryAsync(string id)
            {

            if (string.IsNullOrEmpty(id)) {
                return new DeleteCategoryResponseDto
                {
                    Success = false,
                    Message = "Category ID cannot be empty.",
                    ErrorCode = CategoryErrorCode.EmptyFields
                };
            }

            try
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return new DeleteCategoryResponseDto
                    {
                        Success = false,
                        Message = "Category not found.",
                        ErrorCode = CategoryErrorCode.CategoryNotFound
                    };
                }
                await _categoryRepository.DeleteCategoryAsync(id);
                return new DeleteCategoryResponseDto
                {
                    Success = true,
                    Message = "Category deleted successfully."
                };
            }
            catch (Exception e)
            {
                return new DeleteCategoryResponseDto
                {
                    Success = false,
                    Message = $"Error deleting category: {e.Message}",
                    ErrorCode = CategoryErrorCode.Unknown
                };
            }
            }

            public async Task<UpdateCategoryResponseDto> UpdateCategoryAsync(string id, UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return new UpdateCategoryResponseDto
                    {
                        Success = false,
                        Message = "Category ID cannot be empty.",
                        ErrorCode = CategoryErrorCode.EmptyFields
                    };
                }
                if (updateCategoryRequestDto == null || string.IsNullOrEmpty(updateCategoryRequestDto.Name) || updateCategoryRequestDto.ServiceOptions == null || !updateCategoryRequestDto.ServiceOptions.Any())
                {
                    return new UpdateCategoryResponseDto
                    {
                        Success = false,
                        Message = "Invalid category update request.",
                        ErrorCode = CategoryErrorCode.EmptyFields
                    };
                }
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return new UpdateCategoryResponseDto
                    {
                        Success = false,
                        Message = "Category not found.",
                        ErrorCode = CategoryErrorCode.CategoryNotFound
                    };
                }

                existingCategory.Name = updateCategoryRequestDto.Name;
                existingCategory.Description = updateCategoryRequestDto.Description;
                existingCategory.ServiceOptions = updateCategoryRequestDto.ServiceOptions;

                var updatedCategory = await _categoryRepository.UpdateCategoryAsync(id, existingCategory);

                return new UpdateCategoryResponseDto
                {
                    Success = true,
                    Message = "Category updated successfully.",
                };
            }
            catch (Exception e)
            {
                return new UpdateCategoryResponseDto
                {
                    Success = false,
                    Message = $"Error updating category: {e.Message}",
                    ErrorCode = CategoryErrorCode.Unknown
                };
            }
        }

            public async Task<GetAllCategoryResponseDto> GetAllCategoriesAsync()
            {
            try
            {
                List<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
                
                if(categories == null || !categories.Any())
                {
                    return new GetAllCategoryResponseDto
                    {
                        Success = false,
                        Message = "No categories found.",
                        ErrorCode = CategoryErrorCode.CategoryNotFound
                    };
                }
                else
                {
                    return new GetAllCategoryResponseDto
                    {
                        Success = true,
                        Categories = categories,
                        Message = "Categories retrieved successfully."
                    };
                }
            }
            catch (Exception e)
            {
                return new GetAllCategoryResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving categories: {e.Message}",
                    ErrorCode = CategoryErrorCode.Unknown
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
                            Message = "Category not found.",
                            ErrorCode = CategoryErrorCode.CategoryNotFound
                        };
                    
                    }
                }
                catch(Exception e)
                {
                    return new GetByIdCategoryResponseDto
                    {
                        Success = false,
                        Message = $"Error retrieving category: {e.Message}",
                        ErrorCode = CategoryErrorCode.Unknown
                    };
                }
            }

       }
}
