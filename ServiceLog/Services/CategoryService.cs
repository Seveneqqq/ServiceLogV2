using ServiceLog.Models.Domain;
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

            public async Task CreateCategoryAsync(Category category)
            {
                throw new NotImplementedException();
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
