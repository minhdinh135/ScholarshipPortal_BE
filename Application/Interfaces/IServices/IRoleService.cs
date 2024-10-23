using Domain.DTOs.Common;
using Domain.DTOs.Role;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAll();
        Task<PaginatedList<RoleDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
        Task<RoleDto> Get(int id);
        Task<RoleDto> Add(AddRoleDto dto);
        Task<RoleDto> Update(int id, UpdateRoleDto dto);
        Task<RoleDto> Delete(int id);
    }
}