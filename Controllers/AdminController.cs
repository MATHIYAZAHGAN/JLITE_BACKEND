using System.Threading.Tasks;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public AdminController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalOrders = await _context.Orders.CountDocumentsAsync(_ => true);
            var totalProducts = await _context.Products.CountDocumentsAsync(_ => true);
            var totalServices = await _context.ServiceRequests.CountDocumentsAsync(_ => true);
            var pendingOrders = await _context.Orders.CountDocumentsAsync(o => o.OrderStatus == "Pending");

            return Ok(new
            {
                totalOrders,
                totalProducts,
                totalServices,
                pendingOrders,
                totalRevenue = 245990.00m,
                activeContractors = 54
            });
        }
    }
}
