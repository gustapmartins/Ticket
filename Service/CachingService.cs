
using Newtonsoft.Json;
using ServiceStack.Redis;
using Ticket.Enum;
using Ticket.Interface;
using Ticket.Repository.Dao;

namespace Ticket.Service;

public class CachingService : ICachingService
{
    private readonly IRedisClient _redisClient;
    private readonly IFeatureToggleDao _featureToggleDao;

    public CachingService(IRedisClient redisClient, IFeatureToggleDao featureToggleDao)
    {
        _redisClient = redisClient;
        _featureToggleDao = featureToggleDao;
    }

    public async Task<Output> StringGetSet<Output>(string key, Func<Output> function)
    {
        var cacheHabiliy = FeatureToggle();

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

    public bool FeatureToggle()
    {
        try
        {
            string FT_REDIS = "FT_REDIS_TICKETS";

            var findFeatureToggleRedis = _featureToggleDao.FindId(FT_REDIS);

            return findFeatureToggleRedis != null && findFeatureToggleRedis.IsEnabledFeature == FeatureToggleActive.active;
        }
        catch
        {
            return false;
        }
    }
}
