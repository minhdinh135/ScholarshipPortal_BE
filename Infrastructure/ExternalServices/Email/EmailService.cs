using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace Infrastructure.ExternalServices.Email
{
	public class EmailService : IEmailService
	{
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
	}
}
