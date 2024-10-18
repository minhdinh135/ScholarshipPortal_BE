using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Authentication
{
	public class ForgotPasswordDTO
	{
		public string Email { get; set; }
	}

	public class VerifyOtpDTO
	{
		public string Email { get; set; }
		public string Otp { get; set; }
	}

	public class ResetPasswordDTO
	{
		public string Email { get; set; }
		public string NewPassword { get; set; }
	}
}
