using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsStore.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? OperatingVoltage { get; set; }

        [MaxLength(500)]
        public string? SafetyNote { get; set; }

        [MaxLength(500)]
        public string? DatasheetLink { get; set; }

        [MaxLength(500)]
        public string? SampleCodeLink { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [NotMapped]
        public string StockStatus => Quantity switch
        {
            > 10 => "Available",
            > 0 => "LowStock",
            _ => "OutOfStock"
        };
    }
}