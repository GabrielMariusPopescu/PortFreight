namespace PortFreight.Api.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
{
    private const string HeaderName = "X-Correlation-ID";

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
