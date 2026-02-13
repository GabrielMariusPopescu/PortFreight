namespace PortFreight.Application.Validation.Contracts;

public interface IValidator<T>
{
    ValidationResult Validate(T instance);
}
