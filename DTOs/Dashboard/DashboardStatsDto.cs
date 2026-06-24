namespace ElectronicsStore.API.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OutOfStockCount { get; set; }
        public int UnavailableRequestsCount { get; set; }
    }
}