using Ticket.ExceptionFilter;

namespace Ticket.Service;

public abstract class TicketBase
{
    protected async Task<TResult> HandleErrorAsync<TResult>(Func<Task<TResult>> serviceMethod)
    {
        try
        {
            TResult result = await serviceMethod.Invoke();

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
