using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IAccountsService
	{
		Task<IEnumerable<AccountDTO>> GetAll();
		Task<IEnumerable<AccountWithRoleDTO>> GetAllWithRole();
		Task<PaginatedList<AccountDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
		Task<AccountDTO> Get(int id);
		Task<AccountDTO> Add(AccountAddDTO dto);
		Task<AccountDTO> Update(AccountUpdateDTO dto);
		Task<AccountDTO> Delete(int id);
	}
}
