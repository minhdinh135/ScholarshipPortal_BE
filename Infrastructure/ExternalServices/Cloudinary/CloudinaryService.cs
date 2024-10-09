using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.DTOs.UploadImage;

namespace Infrastructure.ExternalServices.Cloudinary;
public class CloudinaryService
{
  private readonly CloudinaryDotNet.Cloudinary _cloudinary;

  public CloudinaryService(CloudinaryDotNet.Cloudinary cloudinary)
  {
    _cloudinary = cloudinary;
  }

  public async Task<string> UploadImage(UploadImageDTO imageUploadDto)
  {
    if (imageUploadDto.File == null || imageUploadDto.File.Length == 0)
    {
      throw new Exception("No file provided.");
    }

    using (var stream = imageUploadDto.File.OpenReadStream())
    {
      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(imageUploadDto.File.FileName, stream),
        Transformation = new Transformation().Crop("limit").Width(1000).Height(1000)
      };

      var uploadResult = await _cloudinary.UploadAsync(uploadParams);

      if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
      {
        var imageUrl = uploadResult.SecureUrl.ToString();
        var publicId = uploadResult.PublicId;

        return imageUrl;
      }
      else
      {
         throw new Exception("Image upload failed. "+uploadResult.Error.Message);
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
      throw new Exception("Failed to delete image.");
    }
  }
}
