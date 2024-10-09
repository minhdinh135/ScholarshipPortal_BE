using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.ExternalServices.Email;

namespace SSAP.API.Controllers
{
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailSender;

		public EmailController(IEmailService emailSender)
		{
			_emailSender = emailSender;
		}

		[HttpPost("SendMail")]
		public async Task<IActionResult> SendMail(
			[FromForm] string receiver,
			[FromForm] string subject,
			[FromForm] string message,
			[FromForm] EmailFile? attachment = null)
		{
			if (string.IsNullOrWhiteSpace(receiver) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
			{
				return BadRequest("Recipient, subject, and message are required.");
			}

			try
			{
				await _emailSender.SendEmailAsync(receiver, subject, message, attachment.file);
				return Ok("Email sent successfully!");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
