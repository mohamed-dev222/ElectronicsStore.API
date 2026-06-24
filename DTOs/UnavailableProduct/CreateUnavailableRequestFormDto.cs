using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ElectronicsStore.API.DTOs.UnavailableProduct
{
    public class CreateUnavailableRequestFormDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string RequestedProductName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Notes { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
