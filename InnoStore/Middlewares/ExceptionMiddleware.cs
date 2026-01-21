using System.Net;
using Domain.Exceptions;
using Presentation.Models.ErrorModels;

namespace InnoStore.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private const string DefaultUnhandledErrorMessage = "Server failed to handle this request.";

    public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, logger);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger)
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
            await context.Response.WriteAsync(new ErrorDetails((int)statusCode, DefaultUnhandledErrorMessage).ToString());
            logger.LogError(exception, exception.Message);
        }
        else
        {
            await context.Response.WriteAsync(new ErrorDetails((int)statusCode, exception.Message).ToString());
        }
    }
}
