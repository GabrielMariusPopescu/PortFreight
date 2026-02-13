namespace PortFreight.Api.Middlewares;

public class ModelValidationFilter : IActionFilter
{
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

    public void OnActionExecuted(ActionExecutedContext context) { }
}
