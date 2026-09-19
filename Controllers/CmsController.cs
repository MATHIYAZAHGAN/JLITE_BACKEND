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
    public class CmsController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IEmailNotificationService _emailService;

        public CmsController(MongoDbContext context, IEmailNotificationService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet("hero")]
        public async Task<IActionResult> GetHero()
        {
            var hero = await _context.HeroBanners.Find(_ => true).FirstOrDefaultAsync();
            if (hero == null)
            {
                hero = new HeroBanner
                {
                    Title = "High-Voltage Power & Smart Electrical Solutions",
                    HighlightText = "Tamil Nadu's Trusted Govt. A-Grade License Up to 33kV",
                    Subtitle = "Manufacturer & Direct Supplier of ISI/CE Certified Switchgear, MCBs, RCCBs, DB Boxes, Smart Touch Switches & Armoured Cables.",
                    BadgeText = "ISI & CE Certified • 5-Year Warranty • 24h Express Dispatch",
                    TrustStats = new System.Collections.Generic.List<TrustStat>
                    {
                        new TrustStat { Value = "50,000+", Label = "Contractors & B2B Buyers" },
                        new TrustStat { Value = "33kV", Label = "A-Grade Govt. License" },
                        new TrustStat { Value = "40+", Label = "Countries Exported" },
                        new TrustStat { Value = "99.9%", Label = "On-Time Dispatch" }
                    },
                    ImageUrl = "assets/hero-bg.jpg"
                };
            }
            return Ok(hero);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects.Find(_ => true).ToListAsync();
            return Ok(projects);
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients.Find(_ => true).ToListAsync();
            return Ok(clients);
        }

        [HttpPost("contact")]
        public async Task<IActionResult> SubmitContact([FromBody] ContactSubmission model)
        {
            model.CreatedAt = DateTime.UtcNow;
            await _context.ContactSubmissions.InsertOneAsync(model);

            // Send Real Email Notification
            await _emailService.SendServiceNotificationAsync("jlite2025@gmail.com", "CONTACT-" + DateTime.UtcNow.Ticks, "Direct Inquiry: " + model.Subject);

            return Ok(new { success = true, message = "Message received successfully. Our engineering team will respond within 2 hours." });
        }
    }
}
