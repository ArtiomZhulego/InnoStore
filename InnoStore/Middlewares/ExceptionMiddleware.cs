using System.Net;
using Domain.Exceptions;
using Presentation.Models.ErrorModels;

namespace InnoStore.Middlewares;

public class ExceptionMiddleware
{
    private const string DEFAULT_UNHANDLED_ERROR_MESSAGE = "Server failed to handle this request.";

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            await context.Response.WriteAsync(new ErrorDetails((int)statusCode, DEFAULT_UNHANDLED_ERROR_MESSAGE).ToString());
            logger.LogError(exception, exception.Message);
        }
        else
        {
            await context.Response.WriteAsync(new ErrorDetails((int)statusCode, exception.Message).ToString());
        }
    }
}
