using ElectronicsStore.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicsStore.API.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IUnavailableProductRepository UnavailableProductRequests { get; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}