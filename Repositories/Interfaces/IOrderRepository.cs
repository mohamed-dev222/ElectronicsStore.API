using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetWithItemsAsync(int id);
        Task<IEnumerable<Order>> GetAllWithItemsAsync(int page, int pageSize, OrderStatus? status = null);
        Task<int> CountAsync(OrderStatus? status = null);
    }
}