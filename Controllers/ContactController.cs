using JLITE.API.Models;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IEmailService emailService, ILogger<ContactController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact([FromBody] ContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _emailService.SendContactEmailAsync(request);

                if (success)
                {
                    return Ok(new { message = "Contact form submitted successfully" });
                }

                return StatusCode(500, new { message = "Failed to send email. Please try again later." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing contact form");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
