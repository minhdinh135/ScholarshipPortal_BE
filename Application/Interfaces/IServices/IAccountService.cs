using Domain.DTOs.Account;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IAccountsService
	{
		Task<IEnumerable<Account>> GetAll();
		Task<Account> Get(int id);
		Task<Account> Add(AccountAddDTO dto);
		Task<Account> Update(AccountUpdateDTO dto);
		Task<Account> Delete(int id);
	}
}
