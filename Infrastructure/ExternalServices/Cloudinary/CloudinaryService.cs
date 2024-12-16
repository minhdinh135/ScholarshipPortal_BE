using System.Net;
using Application.Exceptions;
using Application.Interfaces.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Exception = System.Exception;

namespace Infrastructure.ExternalServices.Cloudinary;

public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly IPdfService _pdfService;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings, 
            IPdfService pdfService)
    {
        var account = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret
        );
        
        _pdfService = pdfService;
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<List<string>> UploadFiles(IFormFileCollection files)
    {
        try
        {
            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                var fileUrl = await UploadFile(file);
                fileUrls.Add(fileUrl);
            }

            return fileUrls;
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message, new FileLoadException());
        }
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ServiceException("No file provided");
        }

        using (var stream = file.OpenReadStream())
        {
            RawUploadParams uploadParams;
            if (IsImage(file))
            {
                uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Crop("limit").Width(1000).Height(1000)
                };
            }
            else
            {
                uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
            }

            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    var imageUrl = uploadResult.SecureUrl.ToString();
                    var publicId = uploadResult.PublicId;

                    return imageUrl;
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, new FileProcessingException());
            }
        }

        return null;
    }

    private static bool IsImage(IFormFile file)
    {
        if (file == null)
            return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase) &&
               allowedExtensions.Contains(extension);
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

	public async Task<string> CreateAndUploadScholarshipContract(string applicantName, string scholarshipAmount, string providerName, DateTime deadline)
	{
		try
		{
			var pdfBytes = await _pdfService.GenerateScholarshipContractPdf(applicantName, scholarshipAmount, providerName, deadline);

			using (var stream = new MemoryStream(pdfBytes))
			{
				var uploadParams = new RawUploadParams
				{
					File = new FileDescription("Scholarship_Contract.pdf", stream),
				};

				var uploadResult = await _cloudinary.UploadAsync(uploadParams);

				if (uploadResult.StatusCode == HttpStatusCode.OK)
				{
					return uploadResult.SecureUrl.ToString();
				}

				throw new Exception($"Cloudinary upload failed: {uploadResult.Error?.Message}");
			}
		}
		catch (Exception ex)
		{
			throw new ServiceException("Error generating and uploading scholarship contract", ex);
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
