namespace PortFreight.Api.Middlewares;

/// <summary>
/// Middleware that manages a correlation ID for HTTP requests, allowing for tracking and logging of requests across
/// services.
/// </summary>
/// <remarks>If a correlation ID is not provided by the client, a new GUID is generated. The correlation ID is
/// stored in the HttpContext for downstream access and added to the response headers. It is also included in the
/// logging scope to facilitate tracing of requests.</remarks>
/// <param name="next">The delegate that represents the next middleware in the request processing pipeline.</param>
/// <param name="logger">The logger used to log information and errors related to the correlation ID.</param>
public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
{
    private const string HeaderName = "X-Correlation-ID";

    /// <summary>
    /// Asynchronously processes an HTTP request by assigning a correlation ID for tracking and adds it to both the
    /// request context and response headers.
    /// </summary>
    /// <remarks>If the incoming request does not include a correlation ID header, a new GUID is generated and
    /// used as the correlation ID. The correlation ID is stored in the request context for downstream components and is
    /// included in the response headers to facilitate end-to-end request tracking across distributed systems. This
    /// method is typically used in middleware to enable consistent correlation of logs and diagnostics.</remarks>
    /// <param name="context">The HTTP context for the current request, providing access to request and response data.</param>
    /// <returns>A task that represents the asynchronous operation of processing the HTTP request.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Check if client provided a correlation ID
        string correlationId = context.Request.Headers.TryGetValue(HeaderName, out var cid)
            ? cid.ToString()
            : Guid.NewGuid().ToString();

        // Store in HttpContext for downstream access
        context.Items[HeaderName] = correlationId;

        // Add to response headers
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[HeaderName] = correlationId;
            return Task.CompletedTask;
        });

        // Add to logging scope
        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId
        }))
        {
            await next(context);
        }
    }
}
