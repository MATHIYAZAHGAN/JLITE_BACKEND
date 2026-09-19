using System;
using System.Threading.Tasks;

namespace JLITE.API.Services
{
    public interface IEmailNotificationService
    {
        Task SendOrderConfirmationAsync(string toEmail, string orderNumber, decimal amount);
        Task SendServiceNotificationAsync(string toEmail, string requestNumber, string serviceType);
    }

    public class EmailNotificationService : IEmailNotificationService
    {
        public Task SendOrderConfirmationAsync(string toEmail, string orderNumber, decimal amount)
        {
            Console.WriteLine($"[Email]: Order Confirmation sent to {toEmail} for Order #{orderNumber} (?{amount})");
            return Task.CompletedTask;
        }

        public Task SendServiceNotificationAsync(string toEmail, string requestNumber, string serviceType)
        {
            Console.WriteLine($"[Email]: Service Request Confirmation sent to {toEmail} for {serviceType} #{requestNumber}");
            return Task.CompletedTask;
        }
    }
}
