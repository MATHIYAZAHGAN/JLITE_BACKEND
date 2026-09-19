using System.Collections.Generic;
using System.Threading.Tasks;
using JLITE.API.Models;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public ProductsController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] string? type, [FromQuery] string? search)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.IsActive, true);

            if (!string.IsNullOrWhiteSpace(category) && category != "All")
            {
                filter &= Builders<Product>.Filter.Eq(p => p.CategoryName, category);
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Type, type);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter &= Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(search, "i"))
                        | Builders<Product>.Filter.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(search, "i"));
            }

            var products = await _context.Products.Find(filter).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _context.Products.Find(p => p.Id == id || p.Slug == id).FirstOrDefaultAsync();
            if (product == null) return NotFound(new { message = "Product not found" });
            return Ok(product);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.Find(c => c.IsActive).ToListAsync();
            return Ok(categories);
        }
    }
}
