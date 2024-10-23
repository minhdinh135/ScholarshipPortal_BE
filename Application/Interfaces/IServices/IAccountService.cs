using Domain.DTOs.Account;
using Domain.DTOs.Authentication;
using Domain.DTOs.Common;

namespace Application.Interfaces.IServices
{
	public interface IAccountService
	{
		Task<IEnumerable<AccountDto>> GetAll();
		Task<PaginatedList<AccountDto>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
		Task<AccountDto> GetAccount(int id);
		Task<AccountDto> AddAccount(RegisterDto dto);
		Task<AccountDto> UpdateAccount(int id, UpdateAccountDto dto);
		Task<AccountDto> DeleteAccount(int id);
	}
}
