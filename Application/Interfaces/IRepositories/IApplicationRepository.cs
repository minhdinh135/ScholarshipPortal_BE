namespace Application.Interfaces.IRepositories;

public interface IApplicationRepository : IGenericRepository<Domain.Entities.Application>
{
    Task<IEnumerable<Domain.Entities.Application>> GetByApplicantId(int applicantId);
    Task<IEnumerable<Domain.Entities.Application>> GetByScholarshipProgramId(int scholarshipProgramId);
}