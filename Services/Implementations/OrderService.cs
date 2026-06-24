using AutoMapper;
using ElectronicsStore.API.DTOs.Order;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Services.Interfaces;
using ElectronicsStore.API.UnitOfWork;
using ElectronicsStore.API.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicsStore.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> CreateAsync(OrderCreateDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
            {
                throw new ValidationException("Order must contain at least one item.");
            }

            await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var orderItems = new List<OrderItem>();
                decimal totalPrice = 0m;

                foreach (var itemDto in dto.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                    if (product == null)
                        throw new NotFoundException($"Product {itemDto.ProductId} not found.");

                    if (product.Quantity < itemDto.Quantity)
                        throw new InsufficientStockException($"Insufficient stock for product {product.Name}.");

                    product.Quantity -= itemDto.Quantity;
                    _unitOfWork.Products.Update(product);

                    orderItems.Add(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price
                    });

                    totalPrice += product.Price * itemDto.Quantity;
                }

                var order = new Order
                {
                    CustomerName = dto.CustomerName,
                    Phone = dto.Phone,
                    District = dto.District,
                    AddressDetails = dto.AddressDetails,
                    TotalPrice = totalPrice,
                    Status = OrderStatus.Pending,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = orderItems
                };

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                var createdOrder = await _unitOfWork.Orders.GetWithItemsAsync(order.Id);
                return _mapper.Map<OrderReadDto>(createdOrder!);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginationResult<OrderReadDto>> GetAllAsync(int page, int pageSize, OrderStatus? status)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var orders = await _unitOfWork.Orders.GetAllWithItemsAsync(page, pageSize, status);
            var totalCount = await _unitOfWork.Orders.CountAsync(status);

            return new PaginationResult<OrderReadDto>
            {
                Items = _mapper.Map<IEnumerable<OrderReadDto>>(orders),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<OrderReadDto> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetWithItemsAsync(id);
            if (order == null)
                throw new NotFoundException("Order not found.");

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> UpdateStatusAsync(int id, OrderStatus status)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException("Order not found.");

            order.Status = status;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();

            var updatedOrder = await _unitOfWork.Orders.GetWithItemsAsync(id);
            return _mapper.Map<OrderReadDto>(updatedOrder!);
        }

        public Task<OrderReadDto> CancelAsync(int id)
        {
            return UpdateStatusAsync(id, OrderStatus.Cancelled);
        }
    }
}