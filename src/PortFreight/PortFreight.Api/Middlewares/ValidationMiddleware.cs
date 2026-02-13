namespace PortFreight.Api.Middlewares;

public class ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest &&
            context.Items.ContainsKey("ValidationErrors"))
        {
            var errors = context.Items["ValidationErrors"];

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