using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<ScholarshipProgramDto> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task<ScholarshipProgramDto> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id);
}