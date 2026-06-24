using ElectronicsStore.API.Data;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Order?> GetWithItemsAsync(int id)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllWithItemsAsync(int page, int pageSize, OrderStatus? status = null)
        {
            var queryable = _dbSet
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (status.HasValue)
            {
                queryable = queryable.Where(o => o.Status == status.Value);
            }

            return await queryable
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync(OrderStatus? status = null)
        {
            var queryable = _dbSet.AsQueryable();

            if (status.HasValue)
            {
                queryable = queryable.Where(o => o.Status == status.Value);
            }

            return await queryable.CountAsync();
        }
    }
}