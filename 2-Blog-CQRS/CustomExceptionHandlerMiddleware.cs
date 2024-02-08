using System.Net;
using _2_Blog_CQRS.Common;
using FluentValidation;

namespace _2_Blog_CQRS;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            while (ex is AggregateException && ex.InnerException is not null)
                ex = ex.InnerException;

            context.Response.StatusCode = ex switch
            {
                ValidationException _ => (int)HttpStatusCode.BadRequest,
                NotFoundException _ => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await context.Response.WriteAsync(ex.Message);
        }
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}