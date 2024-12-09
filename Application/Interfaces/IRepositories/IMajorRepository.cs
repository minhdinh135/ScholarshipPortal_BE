using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IMajorRepository : IGenericRepository<Major>
{
    Task<IEnumerable<Major>> GetAllMajors();
    Task<IEnumerable<Major>> GetAllParentMajors();
    Task<IEnumerable<Major>> GetAllSubMajorsByParentMajor(int parentMajorId);
    Task<Major> GetMajorById(int id);
}