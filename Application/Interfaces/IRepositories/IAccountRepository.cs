using Domain.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<IEnumerable<Account>> GetAllWithRole();
    Task<PaginatedList<Account>> GetAllAppliedToScholarship(int scholarshipId, int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<Account> GetAccountById(int id);
    Task<bool> IsAccountValidWithRole(int userId, string role);
}
