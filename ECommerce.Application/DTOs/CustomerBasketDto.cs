using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        public List<BasketItemDto> Items { get; set; } = new();
    }

    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;
    }
}