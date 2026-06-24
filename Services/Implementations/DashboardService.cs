using AutoMapper;
using ElectronicsStore.API.DTOs.Dashboard;
using ElectronicsStore.API.Services.Interfaces;
using ElectronicsStore.API.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DashboardStatsDto> GetStatsAsync()
        {
            var totalProducts = await _unitOfWork.Products.Query().CountAsync();
            var totalCategories = await _unitOfWork.Categories.Query().CountAsync();
            var totalOrders = await _unitOfWork.Orders.Query().CountAsync();
            var totalRevenue = await _unitOfWork.Orders.Query().SumAsync(o => o.TotalPrice);
            var outOfStockCount = await _unitOfWork.Products.Query().CountAsync(p => p.Quantity <= 0);
            var unavailableRequestsCount = await _unitOfWork.UnavailableProductRequests.Query().CountAsync();

            return new DashboardStatsDto
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                OutOfStockCount = outOfStockCount,
                UnavailableRequestsCount = unavailableRequestsCount
            };
        }

        public async Task<IEnumerable<TopProductDto>> GetTopProductsAsync(int count)
        {
            var topProducts = await _unitOfWork.Orders.Query()
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity),
                    Revenue = g.Sum(oi => oi.UnitPrice * oi.Quantity),
                    ProductName = g.Select(oi => oi.Product.Name).FirstOrDefault()
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(count)
                .ToListAsync();

            return topProducts.Select(x => new TopProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName ?? string.Empty,
                TotalSold = x.TotalSold,
                Revenue = x.Revenue
            });
        }

        public async Task<IEnumerable<TopProductDto>> GetSlowProductsAsync(int count)
        {
            var slowProducts = await _unitOfWork.Orders.Query()
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity),
                    Revenue = g.Sum(oi => oi.UnitPrice * oi.Quantity),
                    ProductName = g.Select(oi => oi.Product.Name).FirstOrDefault()
                })
                .OrderBy(x => x.TotalSold)
                .Take(count)
                .ToListAsync();

            return slowProducts.Select(x => new TopProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName ?? string.Empty,
                TotalSold = x.TotalSold,
                Revenue = x.Revenue
            });
        }
    }
}