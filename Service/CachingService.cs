
using Newtonsoft.Json;
using ServiceStack.Redis;
using System.Reflection.Metadata;
using Ticket.Interface;

namespace Ticket.Service;

public class CachingService : ICachingService
{
    private readonly IRedisClient _redisClient;
    private readonly IFeatureToggleService _featureToggleService;

    public CachingService(IRedisClient redisClient, IFeatureToggleService featureToggleService)
    {
        _redisClient = redisClient;
        _featureToggleService = featureToggleService;
    }

    public async Task<Output> StringGetSet<Output>(string key, Func<Output> function)
    {
        var cacheHabiliy = _featureToggleService.FeatureToggleActive(Commons.Constants.FT_REDIS);

        if(cacheHabiliy)
        {
            string resultCache = _redisClient.Get<string>(key);

            if (resultCache != null)
                return JsonConvert.DeserializeObject<Output>(resultCache)!;

            Output register = function.Invoke();

            if (register != null)
                _redisClient.Set(key, JsonConvert.SerializeObject(register), TimeSpan.FromHours(1));

            return register;
        }

        return function.Invoke();
    }
}
