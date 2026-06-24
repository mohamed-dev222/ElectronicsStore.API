using ElectronicsStore.API.DTOs.UnavailableProduct;
using ElectronicsStore.API.Helpers;
using Microsoft.AspNetCore.Http;

namespace ElectronicsStore.API.Services.Interfaces
{
    public interface IUnavailableProductService
    {
        Task<UnavailableRequestResponseDto> CreateAsync(CreateUnavailableRequestFormDto dto);
        Task<PaginationResult<UnavailableRequestResponseDto>> GetAllAsync(int page, int pageSize, bool? isFulfilled);
        Task<UnavailableRequestResponseDto> MarkAsFulfilledAsync(int id);
    }
}