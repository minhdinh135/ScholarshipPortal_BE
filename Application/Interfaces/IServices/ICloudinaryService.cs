using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface ICloudinaryService
{
    Task<string> UploadImage(IFormFile file);

    Task<string> UploadRaw(IFormFile file);

    Task<string> DeleteImage(string publicId);

    Task<string> DeleteFile(string publicId);
}