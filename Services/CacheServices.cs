
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Api.Services
{
    public class CacheServices : ICacheServices
    {   
        private readonly IDistributedCache ?  _cache;

        public CacheServices(IDistributedCache cache)
        {
            _cache = cache;
        }
        public  T? GetData<T>(string key)
        {
            var data =  _cache.GetString(key);

            return data == null ? default(T) : JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            var option = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),    
            };

            _cache?.SetString(key, JsonSerializer.Serialize(data), option);
        }
    }
}