using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IScholarshipProgramRepository : IGenericRepository<ScholarshipProgram>
{
    Task<IEnumerable<ScholarshipProgram>> GetAllScholarshipPrograms();

    Task<ScholarshipProgram> GetScholarsipProgramById(int id);

    Task DeleteRelatedInformation(ScholarshipProgram scholarshipProgram);
}