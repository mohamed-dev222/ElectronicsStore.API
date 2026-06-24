using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Repositories.Interfaces
{
    public interface IUnavailableProductRepository : IGenericRepository<UnavailableProductRequest>
    {
        Task<IEnumerable<UnavailableProductRequest>> GetAllAsync(int page, int pageSize, bool? isFulfilled = null);
    }
}