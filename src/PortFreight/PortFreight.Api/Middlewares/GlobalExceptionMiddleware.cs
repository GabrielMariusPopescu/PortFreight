namespace PortFreight.Api.Middlewares;

/// <summary>
/// Middleware that handles exceptions globally for the application, logging errors and returning appropriate HTTP
/// responses.
/// </summary>
/// <remarks>This middleware catches exceptions thrown by downstream middleware and logs them. It returns a JSON
/// response with an appropriate status code based on the type of exception encountered.</remarks>
/// <param name="next">The delegate that represents the next middleware in the request processing pipeline.</param>
/// <param name="logger">The logger used to log unhandled exceptions that occur during request processing.</param>
public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    /// <summary>
    /// Invokes the next middleware in the HTTP request pipeline asynchronously and handles any unhandled exceptions
    /// that occur during execution.
    /// </summary>
    /// <remarks>If an exception is thrown by the next middleware, it is logged and processed by the exception
    /// handling logic to ensure a consistent error response.</remarks>
    /// <param name="context">The HTTP context for the current request, which encapsulates all HTTP-specific information about an individual
    /// HTTP request.</param>
    /// <returns>A task that represents the asynchronous operation of invoking the next middleware and handling exceptions.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            ArgumentNullException => (int)HttpStatusCode.BadRequest,
            ArgumentException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new ErrorResponse
        {
            Message = ex.Message,
            Detail = $"Inner Exception: {ex.InnerException?.Message}\n" +
                     $"CorrelationId: {context.Items["X-Correlation-ID"]?.ToString()}",
            StatusCode = statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
