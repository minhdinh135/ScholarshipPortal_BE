using Domain.DTOs.Applicant;

namespace Application.Interfaces.IServices;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantProfileDto>> GetAll();
    Task<ApplicantProfileDto> Get(int id);
    Task<AddApplicantProfileDto> Add(AddApplicantProfileDto dto);
    Task<ApplicantProfileDto> Update(int id, UpdateApplicantProfileDto dto);
    Task<ApplicantProfileDto> Delete(int id);
}