using System.Text;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Gradeo.CoreApplication.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;
    public CacheService(IDistributedCache distributedCache) => this.distributedCache = distributedCache;
    public TResult GetOrCreateUserScopeAsync<T, TResult>(string key, Func<T, TResult> valueFunc, T parameter)
    {
        try
        {
            var cacheObj = distributedCache.Get(key);
            if (cacheObj != null)
            {
                var serializedObject = Encoding.UTF8.GetString(cacheObj);
                var obj = JsonConvert.DeserializeObject<TResult>(serializedObject);
                return obj;
            }
            else
            {
                var result = valueFunc.Invoke(parameter);
                var serializedObject = JsonConvert.SerializeObject(result);
                var newCacheObj = Encoding.UTF8.GetBytes(serializedObject);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                distributedCache.Set(key, newCacheObj, options);
                return result;
            }
        }
        catch (Exception ex)
        {
            return valueFunc.Invoke(parameter);
        }

    }
    
    public void Remove(string key)
    {
        distributedCache.Remove(key);
    }
}