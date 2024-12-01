using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IElasticService<T> where T : class
{
    Task CreateIndex(string indexName);

    Task CreateScholarshipIndex();

    Task<bool> AddOrUpdate(T entity, string indexName);

    Task<bool> AddOrUpdateScholarship(ScholarshipProgramElasticDocument scholarship);

    Task<bool> AddOrUpdateBulkScholarship(IEnumerable<ScholarshipProgramElasticDocument> entities);

    Task<bool> AddOrUpdateBulk(IEnumerable<T> entities, string indexName);

    Task<T> Get(int key);

    Task<List<T>?> GetAll();

    Task<bool> Remove(string key);

    Task<long?> RemoveAll();

    Task<List<ScholarshipProgramElasticDocument>> SearchScholarships(ScholarshipSearchOptions scholarshipSearchOptions);

    Task<List<string>> SuggestScholarships(string input);
}