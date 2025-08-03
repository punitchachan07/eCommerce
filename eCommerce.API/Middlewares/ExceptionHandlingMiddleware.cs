using System.Net;
namespace eCommerce.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        { 
            _logger.LogError("Exception occurred: {ExceptionType}", ex.GetType().FullName);

            if(ex.InnerException is not null)
            {
                _logger.LogError("Inner Exception: {InnerExceptionType}", ex.InnerException.GetType().FullName);
            }
            _logger.LogError("Exception Message: {Message}", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new 
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Type = ex.GetType().FullName,
            });
        }
    }
    
}
public static class ExceptionHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
