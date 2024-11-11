using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, IFormFile? file = null);
        Task SendInvoiceReceipt(string recipientEmail, decimal amount, string invoiceId);
    }
}