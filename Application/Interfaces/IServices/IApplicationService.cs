using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> GetAll();
        Task<PaginatedList<ApplicationDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        Task<ApplicationDto> Get(int id);
        Task<ApplicationDto> Add(AddApplicationDto dto);
        Task<ApplicationDto> Update(int id, UpdateApplicationDto dto);
        Task<ApplicationDto> Delete(int id);
    }
}