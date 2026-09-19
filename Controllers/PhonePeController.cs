using System;
using System.Threading.Tasks;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/payments/phonepe")]
    public class PhonePeController : ControllerBase
    {
        private readonly IPhonePeService _phonePeService;
        private readonly MongoDbContext _context;

        public PhonePeController(IPhonePeService phonePeService, MongoDbContext context)
        {
            _phonePeService = phonePeService;
            _context = context;
        }

        [HttpPost("initiate")]
        public async Task<IActionResult> Initiate([FromBody] PhonePeInitDto model)
        {
            var txnId = "TXN_" + DateTime.UtcNow.Ticks;
            var response = await _phonePeService.InitiatePaymentAsync(
                txnId,
                model.Amount,
                model.CallbackUrl ?? "https://www.jliteengineers.com/api/payments/phonepe/webhook",
                model.RedirectUrl ?? "https://www.jliteengineers.com/cart?paymentStatus=success",
                model.Phone
            );

            return Ok(response);
        }

        [HttpGet("verify/{transactionId}")]
        public async Task<IActionResult> Verify(string transactionId)
        {
            var isVerified = await _phonePeService.VerifyPaymentStatusAsync(transactionId);
            return Ok(new { transactionId, status = isVerified ? "PAYMENT_SUCCESS" : "PAYMENT_FAILED", success = isVerified });
        }

        [HttpPost("webhook")]
        public IActionResult Webhook([FromBody] object body)
        {
            // PhonePe S2S Webhook listener for real-time order status callback
            return Ok(new { status = "SUCCESS", message = "Webhook received" });
        }
    }

    public class PhonePeInitDto
    {
        public decimal Amount { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? RedirectUrl { get; set; }
        public string? CallbackUrl { get; set; }
    }
}
