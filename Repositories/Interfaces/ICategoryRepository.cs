using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> CategoryExistsAsync(int categoryId);
    }
}