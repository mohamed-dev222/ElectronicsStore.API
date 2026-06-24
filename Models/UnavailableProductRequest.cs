using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.API.Models
{
    public class UnavailableProductRequest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string RequestedProductName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public bool IsFulfilled { get; set; }
    }
}