using ElectronicsStore.API.Data;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _dbSet.AnyAsync(c => c.Id == categoryId);
        }
    }
}