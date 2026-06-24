using ElectronicsStore.API.Data;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Repositories.Implementations
{
    public class UnavailableProductRepository : GenericRepository<UnavailableProductRequest>, IUnavailableProductRepository
    {
        public UnavailableProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<UnavailableProductRequest>> GetAllAsync(int page, int pageSize, bool? isFulfilled = null)
        {
            var queryable = _dbSet.AsQueryable();

            if (isFulfilled.HasValue)
            {
                queryable = queryable.Where(r => r.IsFulfilled == isFulfilled.Value);
            }

            return await queryable
                .OrderByDescending(r => r.RequestDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}