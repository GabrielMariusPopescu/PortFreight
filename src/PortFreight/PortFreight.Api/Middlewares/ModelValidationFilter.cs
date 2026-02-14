namespace PortFreight.Api.Middlewares;

/// <summary>
/// Provides an action filter that validates the model state before an action method executes and returns a bad request
/// response if the model state is invalid.
/// </summary>
/// <remarks>This filter inspects the model state of incoming requests in ASP.NET Core applications. If validation
/// errors are present, it collects them and stores the errors in the HttpContext.Items collection under the key
/// 'ValidationErrors'. The filter then short-circuits the request pipeline by setting the result to a BadRequestResult,
/// preventing the action method from executing. This is typically used to enforce model validation rules and ensure
/// that only valid data is processed by action methods.</remarks>
public class ModelValidationFilter : IActionFilter
{
    /// <summary>
    /// Validates the model state before the action executes and, if invalid, sets the result to a bad request response
    /// containing the validation errors.
    /// </summary>
    /// <remarks>If the model state is invalid, this method populates the HTTP context with the validation
    /// errors and prevents the action from executing by setting a bad request result. This is typically used in ASP.NET
    /// Core MVC applications to enforce model validation prior to action execution.</remarks>
    /// <param name="context">The context for the executing action, which provides access to the action arguments, model state, and HTTP
    /// context.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(pair => pair.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            context.HttpContext.Items["ValidationErrors"] = errors;
            context.Result = new BadRequestResult();
        }
    }

    /// <summary>
    /// Called after an action method has executed to perform additional processing or to modify the result.
    /// </summary>
    /// <remarks>Override this method to implement custom logic that should run after an action method
    /// completes, such as logging, auditing, or modifying the response before it is sent to the client.</remarks>
    /// <param name="context">The context for the executed action, containing information about the action result and related data.</param>
    public void OnActionExecuted(ActionExecutedContext context) { }
}
