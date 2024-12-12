using Domain.DTOs.ScholarshipProgram;

namespace Application.Interfaces.IServices;

public interface IElasticService<T> where T : class
{
    Task CreateIndex(string indexName);

    Task<bool> AddOrUpdate(T entity, string indexName);

    Task<bool> AddOrUpdateScholarship(ScholarshipProgramElasticDocument scholarship);

    Task<bool> AddOrUpdateBulkScholarship(IEnumerable<ScholarshipProgramElasticDocument> entities);

    Task<long?> RemoveAllScholarships();

    Task<List<ScholarshipProgramElasticDocument>> SearchScholarships(ScholarshipSearchOptions scholarshipSearchOptions);

    Task<List<string>> SuggestScholarships(string input);
}