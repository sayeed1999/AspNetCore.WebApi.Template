using System.Data;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Infrastructure.ExceptionHandlers;

/// <summary>
///     Alternate way to handling exceptions rather than middleware which needs a try catch to catch exceptions.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string title;
        string type;
        HttpStatusCode code;

        switch (exception)
        {
            case ArgumentNullException:
                code = HttpStatusCode.BadRequest;
                title = "Bad Request";
                type = "https://httpstatuses.com/400";
                break;
            case InvalidOperationException:
                code = HttpStatusCode.BadRequest;
                title = "Bad Request";
                type = "https://httpstatuses.com/400";
                break;
            case DuplicateNameException:
                code = HttpStatusCode.Conflict;
                title = "Conflict";
                type = "https://httpstatuses.com/409";
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                title = "Internal Server Error";
                type = "https://httpstatuses.com/500";
                break;
        }

        ProblemDetails problemDetails = new()
        {
            Status = (int?)code, Title = title, Type = type, Detail = exception.Message
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;

        string result = JsonSerializer.Serialize(problemDetails);

        await httpContext.Response.WriteAsync(result, cancellationToken);

        return true;
    }
}
