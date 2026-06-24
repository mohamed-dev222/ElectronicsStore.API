namespace ElectronicsStore.API.DTOs.Order
{
    public class CreateOrderItemDto
    {
        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}