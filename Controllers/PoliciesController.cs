using System.Threading.Tasks;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public PoliciesController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var policy = await _context.Policies.Find(p => p.Type == type).FirstOrDefaultAsync();
            if (policy == null) return NotFound(new { message = "Policy not found" });
            return Ok(policy);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var policies = await _context.Policies.Find(_ => true).ToListAsync();
            return Ok(policies);
        }
    }
}
