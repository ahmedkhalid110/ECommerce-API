namespace ECommerce.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItem> Items { get; set; } = new();

        public CustomerBasket() { }

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }

    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}