using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IScholarshipProgramRepository : IGenericRepository<ScholarshipProgram>
{
    Task<PaginatedList<ScholarshipProgram>> GetAllScholarshipPrograms(ListOptions listOptions);

    Task<ScholarshipProgram> GetScholarsipProgramById(int id);

    Task DeleteRelatedInformation(ScholarshipProgram scholarshipProgram);
}