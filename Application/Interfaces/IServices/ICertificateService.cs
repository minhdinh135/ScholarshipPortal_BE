using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface ICertificateService
{
    Task<IEnumerable<CertificateDto>> GetAllCertificates();
    Task<CertificateDto> GetCertificateById(int id);
}