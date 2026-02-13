namespace PortFreight.Api.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
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
