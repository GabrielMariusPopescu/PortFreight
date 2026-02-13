namespace PortFreight.Application.Validation.Implementation;

public abstract class BaseValidator<T> : IValidator<T>
{
    protected readonly ValidationResult Result = new();

    public abstract void ValidateRules(T instance);

    public ValidationResult Validate(T instance)
    {
        ValidateRules(instance);
        return Result;
    }

    protected void AddError(string field, string message)
    {
        if (!Result.Errors.ContainsKey(field))
            Result.Errors[field] = [];

        Result.Errors[field] = [.. Result.Errors[field], message];
    }
}
