using System.Data;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web.Infrastructure;

/// <summary>
///     Global Exception handler using old way (not used currently due to using IExceptionHandlers for exceptions..
///     Responsible for handling unhandled exceptions in the entire system as the last fallback for exceptions.
/// </summary>
/// <param name="next"></param>
public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string result = string.Empty;
        string title = string.Empty;
        string type = string.Empty;
        HttpStatusCode code = HttpStatusCode.InternalServerError;

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
            Status = (int?)code,
            Title = title,
            Type = type,
            Detail = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(problemDetails);
        }

        return context.Response.WriteAsync(result);
    }
}
