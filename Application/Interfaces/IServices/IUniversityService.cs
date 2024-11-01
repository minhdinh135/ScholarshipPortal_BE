using Domain.DTOs.University;

namespace Application.Interfaces.IServices;

public interface IUniversityService
{
    Task<IEnumerable<UniversityDto>> GetAllUniversities();
    Task<UniversityDto> GetUniversityById(int id);
}