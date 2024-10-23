using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Account>> GetAllWithRole()
    {
        var accounts = await _dbContext.Accounts
            .AsNoTracking()
            .AsSplitQuery()
            .Include(a => a.Role)
            .ToListAsync();

        return accounts;
    }
}