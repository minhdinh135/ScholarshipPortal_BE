using System.Net;
using Application.Interfaces.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices.Cloudinary;

public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        var account = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<string> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("No file provided.");
        }

        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Crop("limit").Width(1000).Height(1000)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                var imageUrl = uploadResult.SecureUrl.ToString();
                var publicId = uploadResult.PublicId;

                return imageUrl;
            }
            else
            {
                throw new Exception("Image upload failed. " + uploadResult.Error.Message);
            }
        }
    }

    public async Task<string> UploadRaw(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("No file provided.");
        }

        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new RawUploadParams // Use RawUploadParams for non-media files
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                var imageUrl = uploadResult.SecureUrl.ToString();
                var publicId = uploadResult.PublicId;

                return imageUrl;
            }
            else
            {
                throw new Exception("File upload failed. " + uploadResult.Error.Message);
            }
        }
    }

    // Delete Image Endpoint
    public async Task<string> DeleteImage(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);

        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

        if (deletionResult.Result == "ok")
        {
            return "Image deleted successfully.";
        }
        else
        {
            throw new Exception("Failed to delete image." + deletionResult.Error.Message);
        }
    }

    // Delete Image Endpoint
    public async Task<string> DeleteFile(string publicId)
    {
        var deletionParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Raw // Specify the resource type as 'raw' for non-media files
        };

        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

        if (deletionResult.Result == "ok")
        {
            return "File deleted successfully.";
        }
        else
        {
            throw new Exception("Failed to delete file." + deletionResult.Error.Message);
        }
    }
}