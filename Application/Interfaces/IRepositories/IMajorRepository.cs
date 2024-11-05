using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IMajorRepository : IGenericRepository<Major>
{
    Task<IEnumerable<Major>> GetAllMajors();
    Task<Major> GetMajorById(int id);
}