using Domain.DTOs.Common;
using Domain.DTOs.Role;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleDTO>> GetAll();
    Task<PaginatedList<RoleDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
		Task<RoleDTO> Get(int id);
		Task<RoleDTO> Add(RoleAddDTO dto);
		Task<RoleDTO> Update(RoleUpdateDTO dto);
		Task<RoleDTO> Delete(int id);
	}
}
