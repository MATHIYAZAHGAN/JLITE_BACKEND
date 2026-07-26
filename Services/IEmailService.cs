using JLITE.API.Models;

namespace JLITE.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactEmailAsync(ContactRequest request);
        Task<bool> SendQuoteEmailAsync(QuoteRequest request);
    }
}
