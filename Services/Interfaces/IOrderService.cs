using ElectronicsStore.API.DTOs.Order;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateAsync(OrderCreateDto dto);
        Task<PaginationResult<OrderReadDto>> GetAllAsync(int page, int pageSize, OrderStatus? status);
        Task<OrderReadDto> GetByIdAsync(int id);
        Task<OrderReadDto> UpdateStatusAsync(int id, OrderStatus status);
        Task<OrderReadDto> CancelAsync(int id);
    }
}