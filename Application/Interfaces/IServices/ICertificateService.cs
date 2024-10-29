using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface ICertificateService
{
    Task<IEnumerable<CertificateDto>> GetAllCertficates();
    Task<CertificateDto> GetCertificateById(int id);
}