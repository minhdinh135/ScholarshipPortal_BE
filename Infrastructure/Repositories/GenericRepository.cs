using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.DTOs.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ScholarshipContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository()
    {
        _dbContext = new ScholarshipContext();
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T> DeleteById(params int[] keys)
    {
        var entity = await GetById(keys);

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        var list = await query.AsNoTracking().AsSplitQuery().ToListAsync();

        return list;
    }

    public async Task<PaginatedList<T>> GetPaginatedList(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var query = _dbContext.Set<T>().AsNoTracking().AsSplitQuery();

        if (!string.IsNullOrEmpty(sortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<T>(sortBy);
            query = sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _dbContext.Set<T>().CountAsync();

        return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
    }

    public async Task<T> GetById(params int[] keys)
    {
        if (keys.Length == 1)
        {
            var entity1 = await _dbSet.FindAsync(keys[0]);
            if (entity1 != null)
            {
                _dbContext.Entry(entity1).State = EntityState.Detached;
            }

            return entity1;
        }

        var entity = await _dbSet.FindAsync(keys[0], keys[1]);
        if (entity != null)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbContext.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}