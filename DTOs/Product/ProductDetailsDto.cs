namespace ElectronicsStore.API.DTOs.Product
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? OperatingVoltage { get; set; }
        public string? SafetyNote { get; set; }
        public string? DatasheetLink { get; set; }
        public string? SampleCodeLink { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
