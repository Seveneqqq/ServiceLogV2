using MongoDB.Driver;
using ServiceLog.Data;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoDbContext _mongoDbContext;

        public CategoryRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;        
        }

        public async Task CreateCategoryAsync(Category category)
        {  
            await _mongoDbContext.Categories.InsertOneAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _mongoDbContext.Categories.DeleteOneAsync(id);   
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _mongoDbContext.Categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _mongoDbContext.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category> UpdateCategoryAsync(string id, Category category)
        {
            return await _mongoDbContext.Categories.FindOneAndReplaceAsync(
                c => c.Id == id,
                category,
                new FindOneAndReplaceOptions<Category>
                {
                    ReturnDocument = ReturnDocument.After
                });
        }
    }
}
