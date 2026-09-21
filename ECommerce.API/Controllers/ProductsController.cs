using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Data;
using ECommerce.Core.Entities;
using ECommerce.Core.Specifications;
using ECommerce.Application.DTOs;
using ECommerce.Application.Helpers;

namespace ECommerce.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (specParams.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == specParams.CategoryId);

            if (!string.IsNullOrEmpty(specParams.Search))
                query = query.Where(p => p.Name.ToLower().Contains(specParams.Search));

            query = specParams.Sort switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };

            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((specParams.PageIndex - 1) * specParams.PageSize)
                .Take(specParams.PageSize)
                .Select(p => new ProductToReturnDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    PictureUrl = p.PictureUrl,
                    Category = p.Category.Name,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, totalItems, products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound(new { Message = "Product not found" });

            return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                Category = product.Category.Name,
                StockQuantity = product.StockQuantity
            };
        }
    }
}