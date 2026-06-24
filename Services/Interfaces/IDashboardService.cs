using ElectronicsStore.API.DTOs.Dashboard;

namespace ElectronicsStore.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatsAsync();
        Task<IEnumerable<TopProductDto>> GetTopProductsAsync(int count);
        Task<IEnumerable<TopProductDto>> GetSlowProductsAsync(int count);
    }
}