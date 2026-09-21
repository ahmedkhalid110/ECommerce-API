using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ECommerce.Infrastructure.Data;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Application.DTOs;

namespace ECommerce.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IBasketRepository _basketRepo;

        public OrdersController(StoreContext context, IBasketRepository basketRepo)
        {
            _context = context;
            _basketRepo = basketRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var basket = await _basketRepo.GetBasketAsync(orderDto.BasketId);

            if (basket == null) return BadRequest("Basket not found");

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _context.Products.FindAsync(item.Id);
                if (productItem != null)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = productItem.Id,
                        ProductName = productItem.Name,
                        PictureUrl = productItem.PictureUrl,
                        Price = productItem.Price,
                        Quantity = item.Quantity
                    };
                    items.Add(orderItem);
                }
            }

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                OrderItems = items,
                BuyerEmail = email!,
                ShipToAddress = orderDto.ShipToAddress,
                Subtotal = subtotal
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _basketRepo.DeleteBasketAsync(orderDto.BasketId);

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.BuyerEmail == email)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders);
        }
    }
}