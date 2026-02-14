namespace PortFreight.Api.Middlewares;

/// <summary>
/// Provides extension methods for configuring middleware components in the application's request processing pipeline.
/// </summary>
/// <remarks>These extension methods enable the integration of global exception handling and correlation ID
/// tracking middleware into an ASP.NET Core application. Use these methods to add standardized error handling and
/// request correlation features across all HTTP requests.</remarks>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds middleware to the application's request pipeline that handles unhandled exceptions globally.
    /// </summary>
    /// <remarks>This middleware intercepts unhandled exceptions, allowing for centralized error handling such
    /// as logging or returning custom error responses. It should be registered early in the pipeline to ensure
    /// comprehensive exception coverage.</remarks>
    /// <param name="app">The application builder used to configure the request pipeline. Cannot be null.</param>
    /// <returns>The same instance of the application builder, enabling further configuration.</returns>
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app) 
        => app.UseMiddleware<GlobalExceptionMiddleware>();

    /// <summary>
    /// Adds middleware to the application's request pipeline that assigns and tracks a correlation ID for each HTTP
    /// request.
    /// </summary>
    /// <remarks>Use this middleware to enable correlation ID tracking, which helps trace requests across
    /// distributed systems and services. Each request will have a unique correlation ID attached, facilitating
    /// end-to-end diagnostics and logging.</remarks>
    /// <param name="app">The application builder to which the correlation ID middleware is added.</param>
    /// <returns>The same instance of <see cref="IApplicationBuilder"/>, enabling method chaining.</returns>
    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder app) 
        => app.UseMiddleware<CorrelationIdMiddleware>();
}
