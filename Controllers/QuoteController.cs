using JLITE.API.Models;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(IEmailService emailService, ILogger<QuoteController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuote([FromBody] QuoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _emailService.SendQuoteEmailAsync(request);

                if (success)
                {
                    return Ok(new { message = "Quote request submitted successfully" });
                }

                return StatusCode(500, new { message = "Failed to send quote request. Please try again later." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing quote request");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
