namespace PortFreight.Api.Middlewares;

/// <summary>
/// Middleware that logs HTTP request and response details, including method, path, query string, and response status
/// code.
/// </summary>
/// <remarks>This middleware measures the time taken to process the request and logs the details after the
/// response is generated. It is useful for monitoring and debugging HTTP requests.</remarks>
/// <param name="next">The delegate that represents the next middleware in the request processing pipeline.</param>
/// <param name="logger">The logger used to log request and response information.</param>
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    /// <summary>
    /// Processes an HTTP request by invoking the next middleware in the pipeline and logs details about the request and
    /// response.
    /// </summary>
    /// <remarks>Logs the HTTP method, request path, query string, response status code, and the elapsed time
    /// in milliseconds for processing the request. This method is typically used within a middleware component to
    /// provide request and response logging for diagnostic or monitoring purposes.</remarks>
    /// <param name="context">The HTTP context for the current request, containing information about the HTTP request and response.</param>
    /// <returns>A task that represents the asynchronous operation of invoking the next middleware component.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var request = context.Request;
        var method = request.Method;
        var path = request.Path;
        var query = request.QueryString.HasValue ? request.QueryString.Value : string.Empty;

        await next(context);

        stopwatch.Stop();

        var statusCode = context.Response.StatusCode;

        logger.LogInformation(
            "HTTP {Method} {Path}{Query} responded {StatusCode} in {Elapsed} ms",
            method,
            path,
            query,
            statusCode,
            stopwatch.ElapsedMilliseconds
        );
    }
}
