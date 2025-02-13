namespace Core.Interfaces
{
    public interface IResponsecacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string?> GetCachedResponseAsync(string cacheKey);

        Task RemoveCacheByPattern(string pattern);
    }
}