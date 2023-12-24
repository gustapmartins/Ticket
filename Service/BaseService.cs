using Ticket.ExceptionFilter;
using Ticket.Model;

namespace Ticket.Validation;

public abstract class BaseService
{
    protected TResult HandleError<TResult>(Func<TResult> serviceMethod)
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
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    protected ResultOperation<T> CreateSuccessResult<T>(T content)
    {
        return new ResultOperation<T> { Sucesso = true, Content = content, MensagemErro = null };
    }

    protected ResultOperation<T> CreateErrorResult<T>(string mensagemErro)
    {
        return new ResultOperation<T> { Sucesso = false, Content = default, MensagemErro = mensagemErro };
    }
}
