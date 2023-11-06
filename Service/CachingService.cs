using ServiceStack.Redis;
using Ticket.Interface;
using Newtonsoft.Json;

namespace Ticket.Service;

public class CachingService : ICachingService
{
    private readonly IRedisClient _redisClient;
    public CachingService(IRedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public async Task<Output> StringGetSet<Output>(string key, Func<Task<Output>> function)
    {
        string resultCache = _redisClient.Get<string>(key);

        if (resultCache != null)
            return JsonConvert.DeserializeObject<Output>(resultCache);
        
        Output register = await function.Invoke();

        if (register != null)
            _redisClient.Set(key, JsonConvert.SerializeObject(register), TimeSpan.FromHours(1));

        return register;
    }
}
