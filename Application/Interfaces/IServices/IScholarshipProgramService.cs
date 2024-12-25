using Domain.DTOs.Common;
using Domain.DTOs.Expert;
using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IScholarshipProgramService
{
    Task<PaginatedList<ScholarshipProgramDto>> GetAllPrograms(ListOptions listOptions);
    Task<IEnumerable<ScholarshipProgramDto>> GetAllScholarshipPrograms();
    Task<PaginatedList<ScholarshipProgramDto>> GetScholarshipPrograms(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<IEnumerable<ScholarshipProgramDto>> SearchScholarshipPrograms(
        ScholarshipSearchOptions scholarshipSearchOptions);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByFunderId(int funderId);
    Task<IEnumerable<ScholarshipProgramDto>> GetScholarshipProgramsByMajorId(int majorId);
    Task<ScholarshipProgramDto> GetScholarshipProgramById(int id);
    Task<IEnumerable<ExpertDetailsDto>> GetScholarshipProgramExperts(int scholarshipProgramId);
    Task<int> CreateScholarshipProgram(CreateScholarshipProgramRequest createScholarshipProgramRequest);
    Task<int> UpdateScholarshipProgram(int id, UpdateScholarshipProgramRequest updateScholarshipProgramRequest);
    Task ChangeScholarshipProgramStatus(int id, ChangeScholarshipProgramStatusRequest request);
    Task UpdateScholarshipProgramName(int id, string name);
    Task<ScholarshipProgramDto> DeleteScholarshipProgramById(int id);
    Task UpdateScholarshipProgramStatus(int id, string status);
}
