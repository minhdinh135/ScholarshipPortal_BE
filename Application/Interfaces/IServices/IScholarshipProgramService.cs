using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<ScholarshipProgramDto> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task<ScholarshipProgramDto> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id);
}