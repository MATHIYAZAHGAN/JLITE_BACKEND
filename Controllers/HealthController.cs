using System;
using Microsoft.AspNetCore.Mvc;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new
            {
                status = "Healthy",
                service = "JLite Engineers API Engine",
                timestamp = DateTime.UtcNow,
                renderKeepAlive = true
            });
        }
    }
}
