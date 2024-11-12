using Application.Interfaces.IServices;
using Domain.DTOs.ScholarshipProgram;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices.Elastic;

public class ElasticService<T> : IElasticService<T> where T : class
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _elasticSettings;

    public ElasticService(IOptions<ElasticSettings> elasticSettings)
    {
        _elasticSettings = elasticSettings.Value;

        var settings = new ElasticsearchClientSettings(new Uri(_elasticSettings.Url))
            .DefaultIndex(_elasticSettings.DefaultIndex);

        _client = new ElasticsearchClient(settings);
    }

    public async Task CreateIndex(string indexName)
    {
        var indexExist = await _client.Indices.ExistsAsync(indexName);

        if (!indexExist.Exists)
        {
            await _client.Indices.CreateAsync(indexName);
        }
    }

    public async Task<bool> AddOrUpdate(T entity, string indexName)
    {
        // var response = await _client.IndexAsync(entity, idx =>
        //     idx.Index(_elasticSettings.DefaultIndex)
        //         .OpType(OpType.Index));
        var indexExist = await _client.Indices.ExistsAsync(indexName);

        if (!indexExist.Exists)
        {
            await _client.Indices.CreateAsync(indexName);
        }

        var response = await _client.IndexAsync(entity, idx => idx.Index(indexName));

        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulk(IEnumerable<T> entities, string indexName)
    {
        var response = await _client.BulkAsync(b => b.Index(_elasticSettings.DefaultIndex)
            .UpdateMany(entities, (ed, e) =>
                ed.Doc(e).DocAsUpsert(true)));

        return response.IsValidResponse;
    }

    public async Task<T> Get(int key)
    {
        var response = await _client.GetAsync<T>(key, r =>
            r.Index(_elasticSettings.DefaultIndex));

        return response.Source;
    }

    public async Task<List<T>?> GetAll()
    {
        var response = await _client.SearchAsync<T>(s =>
            s.Index(_elasticSettings.DefaultIndex));

        return response.IsValidResponse ? response.Documents.ToList() : default;
    }

    public async Task<bool> Remove(string key)
    {
        var response = await _client.DeleteAsync<T>(key, d =>
            d.Index(_elasticSettings.DefaultIndex));

        return response.IsValidResponse;
    }

    public async Task<long?> RemoveAll()
    {
        var response = await _client.DeleteByQueryAsync<T>(d =>
            d.Indices(_elasticSettings.DefaultIndex)
                .Query(q => q.MatchAll(new MatchAllQuery())));

        return response.IsValidResponse ? response.Deleted : default;
    }

    public async Task<List<ScholarshipProgramElasticDocument>> SearchScholarships(
        ScholarshipSearchOptions scholarshipSearchOptions)
    {
        var response = await _client.SearchAsync<ScholarshipProgramElasticDocument>(s => s
            .Index("scholarships")
            .From(0)
            .Size(10)
            .Query(q => q
                .Bool(b => b
                    .Must(must => must
                            .MultiMatch(match => match
                                .Query(scholarshipSearchOptions.Name)
                                .Fields(new[] { "name" })
                                .Fuzziness(new Fuzziness("AUTO"))
                            ),
                        must => must
                            .MultiMatch(match => match
                                .Query(scholarshipSearchOptions.Status)
                                .Fields(new[] { "status" })
                            ),
                        must => must
                            .MultiMatch(match => match
                                .Query(scholarshipSearchOptions.CategoryName)
                                .Fields(new[] { "categoryName" })
                            ),
                        must => must
                            .Range(range => range
                                .DateRange(dr => dr
                                    .Field(f => f.Deadline)
                                    .Lte(scholarshipSearchOptions.Deadline)
                                )
                            ),
                        must => must
                            .Range(range => range
                                .NumberRange(nr => nr
                                    .Field(f => f.ScholarshipAmount)
                                    .Gte((double?)scholarshipSearchOptions.ScholarshipMinAmount)
                                    .Lte((double?)scholarshipSearchOptions.ScholarshipMaxAmount)
                                )
                            )
                    )
                )));

        return response.Documents.ToList();
    }
}