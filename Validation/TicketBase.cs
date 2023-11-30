using Ticket.ExceptionFilter;

namespace Ticket.Validation;

public abstract class TicketBase
{
    protected TResult HandleErrorAsync<TResult>(Func<TResult> serviceMethod)
    {
        try
        {
            TResult result = serviceMethod.Invoke();

            if (result == null)
            {
                throw new StudentNotFoundException("this value does not exist");
            }

            return result;
        }
        catch (Exception ex)
        {
            if (ex is StudentNotFoundException)
            {
                throw;
            }

            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}
