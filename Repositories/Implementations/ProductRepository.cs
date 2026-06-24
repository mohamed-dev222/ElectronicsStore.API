using ElectronicsStore.API.Data;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetWithCategoryAsync(int id)
        {
            return await _dbSet.Include(p => p.Category)
                               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string? query, int? categoryId, int page, int pageSize)
        {
            var queryable = _dbSet.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryable = queryable.Where(p => p.Name.Contains(query)
                    || (p.Description != null && p.Description.Contains(query))
                    || p.Category.Name.Contains(query));
            }

            if (categoryId.HasValue)
            {
                queryable = queryable.Where(p => p.CategoryId == categoryId.Value);
            }

            return await queryable
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? query, int? categoryId)
        {
            var queryable = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryable = queryable.Where(p => p.Name.Contains(query)
                    || (p.Description != null && p.Description.Contains(query))
                    || p.Category.Name.Contains(query));
            }

            if (categoryId.HasValue)
            {
                queryable = queryable.Where(p => p.CategoryId == categoryId.Value);
            }

            return await queryable.CountAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Set<Category>().AnyAsync(c => c.Id == categoryId);
        }
    }
}