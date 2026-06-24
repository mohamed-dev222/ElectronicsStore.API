namespace ElectronicsStore.API.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string? Description { get; set; }
    }
}