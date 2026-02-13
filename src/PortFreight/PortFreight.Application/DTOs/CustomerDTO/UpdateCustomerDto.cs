namespace PortFreight.Application.DTOs.CustomerDTO;

public record UpdateCustomerDto(
    Guid Id,
    string Name,
    string Email,
    string Phone
);
