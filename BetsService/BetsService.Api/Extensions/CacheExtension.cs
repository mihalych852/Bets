using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BetsService.Api.Extensions
{
    public static class CacheExtension
    {
        private static readonly TimeSpan defaultSlidingExpiration = TimeSpan.FromMinutes(60);

        public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache
            , string key
            , Func<Task<T>> valueFactory
            , ILogger logger
            , TimeSpan? slidingExpiration = null)
        {
            try
            {
                var cachedValue = await cache.GetStringAsync(key);

                if (!string.IsNullOrEmpty(cachedValue))
                {
                    var result = JsonSerializer.Deserialize<T>(cachedValue);

                    return result;
                }
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, $"An error occurred when retrieving data from the cache by key '{key}' or deserializing it.");
            }

            var newValue = await valueFactory.Invoke();

            try
            {
                await cache.SetStringAsync(key, JsonSerializer.Serialize(newValue), new DistributedCacheEntryOptions
                {
                    SlidingExpiration = slidingExpiration ?? defaultSlidingExpiration
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while writing data to the cache (key: '{key}')");
            }

            return newValue;
        }

        public static async Task SetAsync<T>(this IDistributedCache cache
            , string key
            , T newValue
            , ILogger logger
            , TimeSpan? slidingExpiration = null)
        {
            try
            {
                await cache.SetStringAsync(key, JsonSerializer.Serialize(newValue), new DistributedCacheEntryOptions
                {
                    SlidingExpiration = slidingExpiration ?? defaultSlidingExpiration
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while setting data to the cache (key: '{key}')");
            }
        }
    }
}
