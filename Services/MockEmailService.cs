using JLITE.API.Models;

namespace JLITE.API.Services
{
    /// <summary>
    /// Mock email service for development/testing - doesn't actually send emails
    /// </summary>
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;

        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }

        public Task<bool> SendContactEmailAsync(ContactRequest request)
        {
            _logger.LogInformation("📧 [MOCK] Contact Email");
            _logger.LogInformation("   From: {Name} <{Email}>", request.Name, request.Email);
            _logger.LogInformation("   Subject: {Subject}", request.Subject ?? "New Enquiry");
            _logger.LogInformation("   Message: {Message}", request.Message);
            _logger.LogInformation("   ✅ Email would be sent in production");
            
            return Task.FromResult(true);
        }

        public Task<bool> SendQuoteEmailAsync(QuoteRequest request)
        {
            _logger.LogInformation("📧 [MOCK] Quote Request Email");
            _logger.LogInformation("   From: {Name} <{Email}>", request.Name, request.Email);
            _logger.LogInformation("   Phone: {Phone}", request.Phone);
            _logger.LogInformation("   Project: {ProjectType}", request.ProjectType);
            _logger.LogInformation("   Company: {Company}", request.Company ?? "N/A");
            _logger.LogInformation("   Location: {Location}", request.Location ?? "N/A");
            _logger.LogInformation("   ✅ Email would be sent in production");
            
            return Task.FromResult(true);
        }
    }
}
