using Ticket.ExceptionFilter;

namespace Ticket.Service;

public abstract class TicketBase
{
    protected TResult HandleErrorAsync<TResult>(Func<TResult> serviceMethod)
    {
        try
        {
            TResult result = serviceMethod.Invoke();

            if (result == null)
            {
                throw new StudentNotFoundException("Este valor não existe");
            }
               
            return result;
        }
        catch (Exception ex)
        {
            if(ex is StudentNotFoundException)
            {
                throw;   
            }

            throw new StudentNotFoundException("Erro na solicitação", ex);
        }
    }
}
