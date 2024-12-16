using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, IFormFile? file = null);
        Task SendPaymentReceipt(string recipientEmail, decimal amount, string referenceId);
		Task SendEmailWinnerAsync(string mail, string subject, string message, IFormFileCollection? files = null);
	}
}