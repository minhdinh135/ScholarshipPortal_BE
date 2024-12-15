using Domain.DTOs.Applicant;

namespace Application.Interfaces.IServices
{
    public interface IPdfService
    {
        Task<byte[]> GenerateProfileInPdf(ApplicantProfileDetails profile);
        Task<byte[]> GenerateScholarshipContractPdf(string applicantName,
			string scholarshipAmount,
			string scholarshipProviderName,
			DateTime deadline);
	}
}
