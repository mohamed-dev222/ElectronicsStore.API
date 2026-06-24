using AutoMapper;
using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Repositories.Interfaces;
using ElectronicsStore.API.Services.Interfaces;
using ElectronicsStore.API.UnitOfWork;
using ElectronicsStore.API.Exceptions;

namespace ElectronicsStore.API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResult<ProductResponseDto>> GetAllAsync(int page, int pageSize, int? categoryId, string? query)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var products = await _unitOfWork.Products.SearchAsync(query, categoryId, page, pageSize);
            var totalCount = await _unitOfWork.Products.CountAsync(query, categoryId);

            return new PaginationResult<ProductResponseDto>
            {
                Items = _mapper.Map<IEnumerable<ProductResponseDto>>(products),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ProductResponseDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetWithCategoryAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found.");

            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var exists = await _unitOfWork.Categories.CategoryExistsAsync(dto.CategoryId);
            if (!exists)
                throw new NotFoundException("Category not found.");

            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found.");

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _unitOfWork.Categories.CategoryExistsAsync(dto.CategoryId.Value);
                if (!categoryExists)
                    throw new NotFoundException("Category not found.");
            }

            _mapper.Map(dto, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found.");

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}