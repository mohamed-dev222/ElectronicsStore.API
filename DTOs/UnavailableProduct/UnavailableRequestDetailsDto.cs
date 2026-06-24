namespace ElectronicsStore.API.DTOs.UnavailableProduct
{
    public class UnavailableRequestDetailsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; }
        public string? Reason { get; set; }
    }
}
