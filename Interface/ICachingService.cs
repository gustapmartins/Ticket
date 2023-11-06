namespace Ticket.Interface;

public interface ICachingService
{
    Task<Output> StringGetSet<Output>(string key, Func<Task<Output>> function);
}
