using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface ICloudinaryService
{
    Task<List<string>> UploadFiles(IFormFileCollection files);

    Task<string> UploadImage(IFormFile file);

    Task<string> UploadRaw(IFormFile file);

    Task<string> DeleteImage(string publicId);

    Task<string> DeleteFile(string publicId);

	Task<string> CreateAndUploadScholarshipContract(
			string applicantName,
			string scholarshipAmount,
			string scholarshipProviderName,
			DateTime deadline);
}
