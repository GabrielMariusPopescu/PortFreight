namespace PortFreight.Api.Middlewares;

/// <summary>
/// Middleware that intercepts HTTP requests to provide a standardized JSON response when validation errors occur during
/// model binding or request processing.
/// </summary>
/// <remarks>This middleware checks for a Bad Request (HTTP 400) status code and the presence of validation errors
/// in the HTTP context. If validation errors are detected, it formats and returns a JSON response containing error
/// details, a message, status code, and timestamp. This ensures clients receive consistent feedback for validation
/// failures.</remarks>
/// <param name="next">The next middleware delegate in the HTTP request processing pipeline. This parameter cannot be null.</param>
public class ValidationMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Invokes the next middleware in the HTTP request pipeline and returns a JSON response containing validation error
    /// details if the request fails validation.
    /// </summary>
    /// <remarks>If the response status code is 400 (Bad Request) and validation errors are present in the
    /// context, this method writes a JSON response with error details to the client. This allows clients to receive
    /// structured information about validation failures.</remarks>
    /// <param name="context">The HTTP context for the current request, which provides access to request and response information.</param>
    /// <returns>A task that represents the asynchronous operation of processing the HTTP request.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest &&
            context.Items.TryGetValue("ValidationErrors", out var errors))
        {
            var response = new
            {
                Message = "Validation failed",
                Errors = errors,
                StatusCode = 400,
                Timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}