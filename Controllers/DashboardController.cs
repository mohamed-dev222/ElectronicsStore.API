using ElectronicsStore.API.DTOs.Dashboard;
using ElectronicsStore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<DashboardStatsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _dashboardService.GetStatsAsync();
            return Ok(Helpers.ApiResponse<DashboardStatsDto>.SuccessResponse(stats, "Dashboard stats fetched successfully"));
        }

        [HttpGet("top-products")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<IEnumerable<TopProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopProducts([FromQuery] int count = 5)
        {
            var items = await _dashboardService.GetTopProductsAsync(count);
            return Ok(Helpers.ApiResponse<IEnumerable<TopProductDto>>.SuccessResponse(items, "Top products fetched successfully"));
        }

        [HttpGet("slow-products")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<IEnumerable<TopProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSlowProducts([FromQuery] int count = 5)
        {
            var items = await _dashboardService.GetSlowProductsAsync(count);
            return Ok(Helpers.ApiResponse<IEnumerable<TopProductDto>>.SuccessResponse(items, "Slow products fetched successfully"));
        }
    }
}
