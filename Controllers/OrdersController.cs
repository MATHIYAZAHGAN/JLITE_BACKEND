using System;
using System.Threading.Tasks;
using JLITE.API.Models;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public OrdersController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.OrderNumber = "JL-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper();
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            // Compute 18% GST Breakdown (9% CGST + 9% SGST for intrastate Tamil Nadu)
            order.TaxAmount = Math.Round(order.Subtotal * 0.18m, 2);
            order.CGST = Math.Round(order.TaxAmount / 2, 2);
            order.SGST = Math.Round(order.TaxAmount / 2, 2);
            order.TotalAmount = order.Subtotal + order.TaxAmount + order.ShippingFee - order.DiscountAmount;

            await _context.Orders.InsertOneAsync(order);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _context.Orders.Find(o => o.Id == id || o.OrderNumber == id).FirstOrDefaultAsync();
            if (order == null) return NotFound(new { message = "Order not found" });
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            var orders = await _context.Orders.Find(o => o.UserId == userId || o.CustomerEmail == userId)
                                       .SortByDescending(o => o.CreatedAt)
                                       .ToListAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateOrderStatusDto dto)
        {
            var update = Builders<Order>.Update
                .Set(o => o.OrderStatus, dto.OrderStatus)
                .Set(o => o.PaymentStatus, dto.PaymentStatus ?? "Paid")
                .Set(o => o.TrackingNumber, dto.TrackingNumber ?? string.Empty)
                .Set(o => o.UpdatedAt, DateTime.UtcNow);

            var result = await _context.Orders.UpdateOneAsync(o => o.Id == id || o.OrderNumber == id, update);
            return Ok(new { success = result.ModifiedCount > 0 });
        }
    }

    public class UpdateOrderStatusDto
    {
        public string OrderStatus { get; set; } = "Processing";
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
    }
}
