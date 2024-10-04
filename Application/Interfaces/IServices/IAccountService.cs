using Domain.DTOs.Account;
using Domain.Entities;

namespace Application.Interfaces.IServices;
public interface IAccountService
{
  Task<IEnumerable<Account>> GetAll();
  Task<Account> Get(int keys);
  Task<Account> Add(AccountAddDTO dto);
  Task<Account> Update(AccountUpdateDTO dto);
  Task<Account> Delete(int keys);
}
