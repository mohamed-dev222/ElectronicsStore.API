using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.DTOs.Order
{
    public class OrderUpdateStatusDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}