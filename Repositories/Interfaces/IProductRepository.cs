using ElectronicsStore.API.Models;
using ElectronicsStore.API.Helpers;

namespace ElectronicsStore.API.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetWithCategoryAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string? query, int? categoryId, int page, int pageSize);
        Task<int> CountAsync(string? query, int? categoryId);
        Task<bool> CategoryExistsAsync(int categoryId);
    }
}