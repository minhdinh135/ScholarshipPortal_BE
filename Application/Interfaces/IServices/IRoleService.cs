using Domain.DTOs.Role;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IRoleService
	{
		Task<IEnumerable<Role>> GetAll();
		Task<Role> Get(int id);
		Task<Role> Add(RoleAddDTO dto);
		Task<Role> Update(RoleUpdateDTO dto);
		Task<Role> Delete(int id);
	}
}
