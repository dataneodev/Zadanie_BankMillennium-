using Microsoft.Extensions.Caching.Distributed;

namespace Millenium.Client;

internal sealed class HttpService : IHttpService
{
    private const string RestKey = "RestKey";
    private readonly IDistributedCache _cache;

    private readonly IHttpClientFactory _httpClientFactory;

    public HttpService(IHttpClientFactory httpClientFactory,
        IDistributedCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }

    public async Task<string?> GetRestData()
    {
        var cached = await _cache.GetStringAsync(RestKey);
        if (cached != null) return cached;

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:7161/fake");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        var cacheOptions = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        await _cache.SetStringAsync(RestKey, result, cacheOptions);

        return result;
    }
}