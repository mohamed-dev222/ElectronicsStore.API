using ElectronicsStore.API.Data;
using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicsStore.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categories,
            IProductRepository products,
            IOrderRepository orders,
            IUnavailableProductRepository unavailableProductRequests)
        {
            _context = context;
            Categories = categories;
            Products = products;
            Orders = orders;
            UnavailableProductRequests = unavailableProductRequests;
        }

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }
        public IUnavailableProductRepository UnavailableProductRequests { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}