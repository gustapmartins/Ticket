using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ticket.ExceptionFilter;

public class NotImplExceptionFilterAttribute: ExceptionFilterAttribute
{

    private readonly ILogger<NotImplExceptionFilterAttribute> _logger;

    public NotImplExceptionFilterAttribute(ILogger<NotImplExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {

        var result = new ObjectResult(new
        {
            context.Exception.Message,
            context.Exception.Source,
            ExceptionType = context.Exception.GetType().FullName,
        })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        _logger.LogError("Unhandled exception ocurred while executing request: {ex}", context.Exception);

        context.Result = result;
    }
}
