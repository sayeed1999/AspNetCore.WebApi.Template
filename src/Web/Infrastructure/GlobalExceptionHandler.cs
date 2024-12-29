using System.Data;
using System.Net;
using System.Text.Json;
using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Infrastructure;

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
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                title = "The specified resource was not found.";
                type = "https://tools.ietf.org/html/rfc7235#section-3.1";
                break;
            case ForbiddenAccessException:
                code = HttpStatusCode.Forbidden;
                title = "Forbidden";
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                title = "Not Found";
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                break;
            case ValidationException:
                code = HttpStatusCode.UnprocessableEntity;
                title = "Unprocessable entity";
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                break;
            case DuplicateNameException:
                code = HttpStatusCode.Conflict;
                title = "Conflict";
                type = "https://httpstatuses.com/409";
                break;
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
