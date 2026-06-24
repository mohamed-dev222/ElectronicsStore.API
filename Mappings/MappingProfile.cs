using AutoMapper;
using ElectronicsStore.API.DTOs.Category;
using ElectronicsStore.API.DTOs.Dashboard;
using ElectronicsStore.API.DTOs.Order;
using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.DTOs.UnavailableProduct;
using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.StockStatus, opt => opt.MapFrom(src => src.StockStatus));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice));
            CreateMap<OrderCreateDto, Order>();

            CreateMap<UnavailableProductRequest, UnavailableRequestResponseDto>();
            CreateMap<CreateUnavailableRequestFormDto, UnavailableProductRequest>();

            CreateMap<Order, DashboardStatsDto>();
            CreateMap<Product, TopProductDto>();
        }
    }
}