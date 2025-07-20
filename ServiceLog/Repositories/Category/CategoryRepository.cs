using MongoDB.Bson;
using MongoDB.Driver;
using ServiceLog.Data;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.CategoryRepository
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

        public async Task<DeleteResult> DeleteCategoryAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq("_id", new ObjectId(id));
            return await _mongoDbContext.Categories.DeleteOneAsync(filter);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _mongoDbContext.Categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _mongoDbContext.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ReplaceOneResult> UpdateCategoryAsync(string id, Category category)
        {
            return await _mongoDbContext.Categories.ReplaceOneAsync(x => x.Id == id, category);
        }
    }
}
