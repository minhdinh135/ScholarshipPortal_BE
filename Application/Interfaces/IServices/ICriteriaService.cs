using Domain.DTOs.Common;
using Domain.DTOs.Criteria;

namespace Application.Interfaces.IServices;

public interface ICriteriaService
{
    Task<IEnumerable<CriteriaDto>> GetAllCriteria();
    Task<PaginatedList<CriteriaDto>> GetCriteria(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<CriteriaDto> GetCriteriaById(int id);
    Task<CriteriaDto> CreateCriteria(CreateCriteriaRequest createCriteriaRequest);
    Task<CriteriaDto> UpdateCriteria(int id, UpdateCriteriaRequest updateCriteriaRequest);
    Task<CriteriaDto> DeleteCriteriaById(int id);
}