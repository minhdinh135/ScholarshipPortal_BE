namespace Application.Interfaces.IServices;

public interface IElasticService<T> where T : class
{
    Task CreateIndex(string indexName);

    Task<bool> AddOrUpdate(T entity);

    Task<bool> AddOrUpdateBulk(IEnumerable<T> entities, string indexName);

    Task<T> Get(string key);

    Task<List<T>?> GetAll();

    Task<bool> Remove(string key);

    Task<long?> RemoveAll();
}