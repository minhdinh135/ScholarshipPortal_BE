using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs.ScholarshipProgram;

namespace Application.Services;

public class CertificateService : ICertificateService
{
    private readonly IMapper _mapper;
    private readonly ICertificateRepository _certificateRepository;

    public CertificateService(IMapper mapper, ICertificateRepository certificateRepository)
    {
        _mapper = mapper;
        _certificateRepository = certificateRepository;
    }
    public async Task<IEnumerable<CertificateDto>> GetAllCertficates()
    {
        var certificates = await _certificateRepository.GetAll();

        return _mapper.Map<IEnumerable<CertificateDto>>(certificates);
    }

    public async Task<CertificateDto> GetCertificateById(int id)
    {
        var certificate = await _certificateRepository.GetById(id);

        return _mapper.Map<CertificateDto>(certificate);
    }
}