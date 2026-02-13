namespace PortFreight.Application.Validation;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public Dictionary<string, string[]> Errors { get; } = [];
}
