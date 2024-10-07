using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.UploadImage;
public class UploadImageDTO
{
    public IFormFile File { get; set; }
}
