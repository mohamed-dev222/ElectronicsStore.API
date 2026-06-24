namespace ElectronicsStore.API.DTOs.Product
{
    public class UpdateProductDto
    {
        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string? Name { get; set; }

        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
        public int? CategoryId { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0.01, 999999.99)]
        public decimal? Price { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
        public int? Quantity { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string? Description { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string? OperatingVoltage { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string? SafetyNote { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string? DatasheetLink { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string? SampleCodeLink { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string? ImageUrl { get; set; }
    }
}