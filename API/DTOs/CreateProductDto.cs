using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]  
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string PictureUrl { get; set; } = string.Empty;

        [Range(0.0, double.MaxValue, ErrorMessage = "Product price needs to be greater than 0")]
        public decimal Price { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Product quantity needs to be at least 1")]
        public int QuantityInStock { get; set; }
    }
}