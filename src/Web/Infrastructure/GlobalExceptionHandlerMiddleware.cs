using System.Data;
using System.Net;
using System.Text.Json;

namespace AspNetCore.WebApi.Template.Web.Infrastructure;

/// <summary>
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

        HttpStatusCode code = exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            DuplicateNameException => HttpStatusCode.Conflict,
            // add more types that are not handled by IExceptionHandler and should have changed HttpStatusCode
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new
            {
                message = exception.Message, exception = exception.InnerException
            });
        }

        return context.Response.WriteAsync(result);
    }
}
