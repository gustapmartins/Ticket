namespace Ticket.Interface;

public interface ICachingService
{
    Task<Output> StringGetSet<Output>(string key, Func<Output> function);

    bool FeatureToggle();
}
