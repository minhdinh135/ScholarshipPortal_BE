using Microsoft.AspNetCore.Http;

namespace Infrastructure.ExternalServices.Email
{
    public class EmailFile
    {
        public IFormFile? file { get; set; }
    }
}