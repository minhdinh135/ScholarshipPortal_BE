using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IScholarshipProgramRepository : IGenericRepository<ScholarshipProgram>
{
    Task<IEnumerable<ScholarshipProgram>> GetAllScholarshipPrograms();

    Task<ScholarshipProgram> GetScholarsipProgramById(int id);

    Task ClearExistingUniversities(ScholarshipProgram scholarshipProgram);

    Task ClearExistingMajors(ScholarshipProgram scholarshipProgram);
}