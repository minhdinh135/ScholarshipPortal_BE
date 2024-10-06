namespace Application.Interfaces.IRepositories;
public interface IGenericRepository<T> where T : class
{
  Task<IEnumerable<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includes);
  Task<T> GetById(params int[] keys);
  Task<T> Add(T entity);
  Task<T> Update(T entity);
  Task<T> DeleteById(params int[] keys);
}
