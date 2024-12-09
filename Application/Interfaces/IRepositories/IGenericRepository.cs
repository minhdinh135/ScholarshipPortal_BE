using Domain.DTOs.Common;

namespace Application.Interfaces.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includes);

    Task<PaginatedList<T>> GetPaginatedList(Func<IQueryable<T>, IQueryable<T>>[] includes,
        ListOptions listOptions);

    Task<PaginatedList<T>> GetPaginatedList(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<T> GetById(params int[] keys);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> DeleteById(params int[] keys);
}