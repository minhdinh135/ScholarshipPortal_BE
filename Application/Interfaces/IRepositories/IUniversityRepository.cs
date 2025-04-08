using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IUniversityRepository : IGenericRepository<University>
{
    Task<IEnumerable<University>> GetAllUniversities();
    Task<University> GetUniversityById(int id);
}