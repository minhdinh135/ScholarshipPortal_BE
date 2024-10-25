using Microsoft.AspNetCore.Http;


namespace Domain.DTOs.Account
{
	public class ChangeAvatarDTO
	{
		public IFormFile File { get; set; }

	}
}
