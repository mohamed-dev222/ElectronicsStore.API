using System.Collections.Generic;

namespace ElectronicsStore.API.DTOs.Order
{
    public class OrderCreateDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string District { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string AddressDetails { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        public IEnumerable<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
    }
}