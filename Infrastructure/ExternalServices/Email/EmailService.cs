using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.ExternalServices.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string mail, string subject, string message, IFormFile? file = null)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse("portalscholarship6@gmail.com");
        email.From.Add(MailboxAddress.Parse("portalscholarship6@gmail.com"));
        email.To.Add(MailboxAddress.Parse(mail));
        email.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = message
        };

        if (file != null)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            builder.Attachments.Add(file.FileName, stream.ToArray(), ContentType.Parse(file.ContentType));
        }

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
        smtp.Authenticate("portalscholarship6@gmail.com", "mopg jgto lymy jvxw");

        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }

    public async Task SendPaymentReceipt(string recipientEmail, decimal amount, string referenceId)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_emailSettings.Mail);
        email.From.Add(MailboxAddress.Parse(_emailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(recipientEmail));
        email.Subject = "Invoice payment receipt";

        var builder = new BodyBuilder
        {
            HtmlBody = $@"
        <html>
        <body>
            <h2>Thank you for your payment!</h2>
            <p>Hello,</p>
            <p>We have received your payment. Here are the details of your transaction:</p>
            <table>
                <tr><td><strong>Reference ID:</strong></td><td>{referenceId}</td></tr>
                <tr><td><strong>Amount Paid:</strong></td><td>{amount} USD</td></tr>
            </table>
            <br />
            <p>Best regards,</p>
            <p>Scholarship Portal</p>
        </body>
        </html>"
        };

        email.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Mail, _emailSettings.Password);
            await client.SendAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}