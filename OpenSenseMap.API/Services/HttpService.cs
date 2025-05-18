using Microsoft.Extensions.Caching.Memory;

namespace OpenSenseMap.API.Services
{
    public class HttpService(HttpClient httpClient, IMemoryCache cache)
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMemoryCache cache = cache;
        private const string tokenKeyName = "TOKEN";

        public string GetToken()
        {
            // Check if data is in the cache
            if (cache.TryGetValue<string>(tokenKeyName, out var cachedData))
            {
                return cachedData ?? string.Empty;
            }

            // Return a default value if no data is found in the cache  
            return string.Empty;
        }

        public void SetToken(string token)
        {
            // Set the cache options

            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            // Set the data in the cache
            cache.Set(tokenKeyName, token, cacheOptions);
        }

        public void RemoveToken()
        {
            // Remove the token from the cache
            if (cache.TryGetValue<string>(tokenKeyName, out _))
            {
                cache.Remove(tokenKeyName);
            }
        }

        public HttpClient Client
        {
            get
            {
                return httpClient;
            }
        }
    }
}
