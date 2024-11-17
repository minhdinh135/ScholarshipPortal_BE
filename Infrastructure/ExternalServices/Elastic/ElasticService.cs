using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.ScholarshipProgram;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using ServiceException = Application.Exceptions.ServiceException;

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

    public async Task CreateScholarshipIndex()
    {
        var indexExists = await _client.Indices.ExistsAsync("scholarships");

        if (!indexExists.Exists)
        {
            await _client.Indices.CreateAsync("scholarships", c => c
                .Mappings(m => m
                    .Dynamic(DynamicMapping.False)
                    .Properties<ScholarshipProgramElasticDocument>(p => p
                        .SearchAsYouType(t => t.Name)
                        .Text(t => t.Description, c => c
                            .Fields(f => f.Keyword(new PropertyName("keyword"))
                            )
                        )
                        .Text(t => t.CategoryName, c => c
                            .Fields(f => f.Keyword(new PropertyName("keyword"))
                            )
                        )
                        .IntegerNumber(n => n.NumberOfScholarships)
                        .LongNumber(n => n.ScholarshipAmount)
                        .Date(d => d.Deadline)
                        .Text(t => t.Status, c => c
                            .Fields(f => f.Keyword(new PropertyName("keyword"))
                            )
                        )
                    )
                )
            );
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
            await CreateIndex(indexName);
        }

        var response = await _client.IndexAsync(entity, idx => idx.Index(indexName));

        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateScholarship(ScholarshipProgramElasticDocument scholarship)
    {
        var indexExist = await _client.Indices.ExistsAsync("scholarships");

        if (!indexExist.Exists)
        {
            await CreateScholarshipIndex();
        }

        var response = await _client.IndexAsync(scholarship, idx => idx.Index("scholarships"));

        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulk(IEnumerable<T> entities, string indexName)
    {
        var indexExist = await _client.Indices.ExistsAsync(indexName);

        if (!indexExist.Exists)
        {
            await CreateIndex(indexName);
        }

        var response = await _client.BulkAsync(b => b.Index(indexName)
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
        var mustQueries = new List<Action<QueryDescriptor<ScholarshipProgramElasticDocument>>>();

        if (!string.IsNullOrEmpty(scholarshipSearchOptions.Name))
        {
            mustQueries.Add(must => must.MultiMatch(match => match
                .Query(scholarshipSearchOptions.Name)
                .Fields(new[] { "name" })
                .Fuzziness(new Fuzziness("AUTO"))));
        }

        if (!string.IsNullOrEmpty(scholarshipSearchOptions.Status))
        {
            mustQueries.Add(must => must
                .MultiMatch(match => match
                    .Query(scholarshipSearchOptions.Status)
                    .Fields(new[] { "status" })
                ));
        }

        if (!string.IsNullOrEmpty(scholarshipSearchOptions.CategoryName))
        {
            mustQueries.Add(must => must
                .MultiMatch(match => match
                    .Query(scholarshipSearchOptions.CategoryName)
                    .Fields(new[] { "categoryName" })
                ));
        }

        if (scholarshipSearchOptions.Deadline.HasValue)
        {
            mustQueries.Add(must => must
                .Range(range => range
                    .DateRange(dr => dr
                        .Field(f => f.Deadline)
                        .Lte(scholarshipSearchOptions.Deadline)
                    )
                ));
        }

        mustQueries.Add(must => must
            .Range(range => range
                .NumberRange(nr => nr
                    .Field(f => f.ScholarshipAmount)
                    .Gte((double?)scholarshipSearchOptions.ScholarshipMinAmount)
                    .Lte((double?)scholarshipSearchOptions.ScholarshipMaxAmount)
                )
            ));

        var response = await _client.SearchAsync<ScholarshipProgramElasticDocument>(s => s
            .Index("scholarships")
            .From(0)
            .Size(10)
            .Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                )));

        return response.Documents.ToList();
    }

    public async Task<List<string>> SuggestScholarships(string input)
    {
        var response = await _client.SearchAsync<ScholarshipProgramElasticDocument>(s => s
            .Query(q => q.MultiMatch(mm => mm
                    .Query(input)
                    .Type(TextQueryType.BoolPrefix)
                    .Fields(new[] { "name", "name._2gram", "name._3gram" })
                )
            )
        );

        return response.Documents.Select(d => d.Name).ToList();
    }
}
