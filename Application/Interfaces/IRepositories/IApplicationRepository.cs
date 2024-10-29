namespace Application.Interfaces.IRepositories;

public interface IApplicationRepository : IGenericRepository<Domain.Entities.Application>
{
    Task<Domain.Entities.Application> GetWithDocumentsAndAccount(int applicationId);
    Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipId(int scholarshipId);
    Task<IEnumerable<Domain.Entities.Application>> GetByApplicantId(int applicantId);
    Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipProgramId(int scholarshipProgramId);
}
