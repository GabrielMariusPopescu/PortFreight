namespace PortFreight.Application.Validation.Implementation;

public class CreateCustomerValidator : BaseValidator<CreateCustomerDto>
{
    public override void ValidateRules(CreateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            AddError(nameof(dto.Name), "Name is required.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            AddError(nameof(dto.Email), "Email is required.");

        if (!dto.Email.Contains('@'))
            AddError(nameof(dto.Email), "Email must be valid.");

        if (string.IsNullOrWhiteSpace(dto.Phone))
            AddError(nameof(dto.Phone), "Phone is required.");
    }
}