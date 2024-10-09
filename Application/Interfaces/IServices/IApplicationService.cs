using Domain.DTOs.Application;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
	public interface IApplicationService
	{
		Task<IEnumerable<ApplicationDTO>> GetAll();
    Task<PaginatedList<ApplicationDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
		Task<ApplicationDTO> Get(int id);
		Task<ApplicationDTO> Add(ApplicationAddDTO dto);
		Task<ApplicationDTO> Update(ApplicationUpdateDTO dto);
		Task<ApplicationDTO> Delete(int id);
	}
}
