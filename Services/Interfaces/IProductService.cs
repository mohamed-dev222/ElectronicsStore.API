using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.Helpers;

namespace ElectronicsStore.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<PaginationResult<ProductResponseDto>> GetAllAsync(int page, int pageSize, int? categoryId, string? query);
        Task<ProductResponseDto> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
        Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}