using System.Collections.Generic;
using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.DTOs.Order
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string AddressDetails { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
    }
}