using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, IFormFile? file = null);
        Task SendPaymentReceipt(string recipientEmail, decimal amount, string referenceId);
    }
}