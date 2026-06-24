namespace ElectronicsStore.API.DTOs.UnavailableProduct
{
    public class CreateUnavailableRequestDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string RequestedProductName { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}