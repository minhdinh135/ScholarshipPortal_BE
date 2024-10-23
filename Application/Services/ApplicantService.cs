using Application.Interfaces.IServices;
using Domain.DTOs.Applicant;

namespace Application.Services;

public class ApplicantService : IApplicantService
{
    public Task<IEnumerable<ApplicantProfileDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicantProfileDto> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AddApplicantProfileDto> Add(AddApplicantProfileDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicantProfileDto> Update(int id, UpdateApplicantProfileDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicantProfileDto> Delete(int id)
    {
        throw new NotImplementedException();
    }
}