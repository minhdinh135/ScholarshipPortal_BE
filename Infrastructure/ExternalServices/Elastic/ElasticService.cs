using Application.Interfaces.IServices;
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
        if (!_client.Indices.Exists(indexName).Exists)
        {
            await _client.Indices.CreateAsync(indexName);
        }
    }

    public async Task<bool> AddOrUpdate(T entity)
    {
        var response = await _client.IndexAsync(entity, idx =>
            idx.Index(_elasticSettings.DefaultIndex)
                .OpType(OpType.Index));

        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulk(IEnumerable<T> entities, string indexName)
    {
        var response = await _client.BulkAsync(b => b.Index(_elasticSettings.DefaultIndex)
            .UpdateMany(entities, (ed, e) =>
                ed.Doc(e).DocAsUpsert(true)));

        return response.IsValidResponse;
    }

    public async Task<T> Get(string key)
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
}