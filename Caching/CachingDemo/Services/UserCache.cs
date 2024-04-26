using CachingDemo.Interfaces;
using CachingDemo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo.Services
{
    public class UserCache : IUserChace
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "user_value";
        public UserCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task AdduserToCache(IEnumerable<User> user)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(10),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            };
            _memoryCache.Set(cacheKey, user, option);
        }

        public async Task<IEnumerable<User>?> GetUserFromCache()
        {
            var clinetData = _memoryCache.Get<IEnumerable<User>>(cacheKey);
            return clinetData;
        }
    }
}
