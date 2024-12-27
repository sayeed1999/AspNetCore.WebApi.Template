using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Infrastructure.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(
                new ValidationProblemDetails(ex.Errors)
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                }, cancellationToken);

            return true;
        }

        return false;
    }
}
