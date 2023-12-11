
using Newtonsoft.Json;
using ServiceStack.Redis;
using Ticket.Interface;

namespace Ticket.Service;

public class CachingService : ICachingService
{
    private readonly IRedisClient _redisClient;
    public CachingService(IRedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public async Task<Output> StringGetSet<Output>(string key, Func<Output> function)
    {
        string resultCache = _redisClient.Get<string>(key);

        if (resultCache != null)
            return JsonConvert.DeserializeObject<Output>(resultCache)!;

        Output register = function.Invoke();

        if (register != null)
            _redisClient.Set(key, JsonConvert.SerializeObject(register), TimeSpan.FromHours(1));

        return register;
    }

    public async Task<bool> FeatureToggle()
    {
        return true;
    }
}
