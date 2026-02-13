namespace PortFreight.Api.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app) 
        => app.UseMiddleware<GlobalExceptionMiddleware>();

    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder app) 
        => app.UseMiddleware<CorrelationIdMiddleware>();
}
