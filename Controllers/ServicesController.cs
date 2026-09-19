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
    public class ServicesController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public ServicesController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpPost("request")]
        public async Task<IActionResult> SubmitRequest([FromBody] ServiceRequest req)
        {
            req.RequestNumber = "SRV-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
            req.CreatedAt = DateTime.UtcNow;
            await _context.ServiceRequests.InsertOneAsync(req);
            return Ok(req);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _context.ServiceRequests.Find(_ => true)
                                          .SortByDescending(r => r.CreatedAt)
                                          .ToListAsync();
            return Ok(requests);
        }
    }
}
