namespace ElectronicsStore.API.DTOs.UnavailableProduct
{
    public class UnavailableRequestResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string RequestedProductName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Notes { get; set; }
        public bool IsFulfilled { get; set; }
        public DateTime RequestDate { get; set; }
    }
}