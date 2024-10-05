using Domain.DTOs.Role;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IRolesService
	{
		Task<IEnumerable<Role>> GetAll();
		Task<Role> Get(int id);
		Task<Role> Add(RoleAddDTO dto);
		Task<Role> Update(RoleUpdateDTO dto);
		Task<Role> Delete(int id);
	}
}
