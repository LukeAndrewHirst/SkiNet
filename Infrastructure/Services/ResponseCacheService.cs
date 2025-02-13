using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCacheService(IConnectionMultiplexer redis) : IResponsecacheService
    {
        private readonly IDatabase _database = redis.GetDatabase(1);

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string cacheKey)
        {
            var cachedReponse = await _database.StringGetAsync(cacheKey);
            if(cachedReponse.IsNullOrEmpty) return null;

            return cachedReponse;
        }

        public async Task RemoveCacheByPattern(string pattern)
        {
            var server  = redis.GetServer(redis.GetEndPoints().First());
            var keys = server.Keys(database: 1, pattern: $"*{pattern}*").ToArray();

            if(keys.Length != 0)
            {
                await _database.KeyDeleteAsync(keys);
            }
        }
    }
}