using ECommerce.Core.Entities;

namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; } = string.Empty;
        public Address ShipToAddress { get; set; } = null!;
    }
}