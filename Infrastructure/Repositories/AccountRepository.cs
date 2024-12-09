using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.DTOs.Common;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedList<Account>> GetAllAppliedToScholarship(int scholarshipId, int pageIndex, int pageSize, string sortBy, string sortOrder)
    {

        var applications = _dbContext.Set<Domain.Entities.Application>()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(a => a.ScholarshipProgramId == scholarshipId)
            .Include(a => a.Applicant);
        var query = applications.Select(a => a.Applicant).GroupBy(a => a.Id).Select(g => g.FirstOrDefault()) ;
        if (!string.IsNullOrEmpty(sortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<Account>(sortBy);
            query = sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PaginatedList<Account>(items, pageIndex, totalPages);

    }

    public async Task<Account> GetAccountById(int id)
    {
        var account = await _dbContext.Accounts
            .AsSplitQuery()
            .Include(a => a.ApplicantProfile)
            .Include(a => a.FunderProfile)
            .Include(a => a.ExpertProfile)
            .Include(a => a.ProviderProfile)
            .Include(a => a.Role)
            .Include(a => a.Wallet)
            .ThenInclude(w => w.ReceiverTransactions)
            .Include(a => a.Wallet)
            .ThenInclude(w => w.SenderTransactions)
            .FirstOrDefaultAsync(a => a.Id == id);

        return account;
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
