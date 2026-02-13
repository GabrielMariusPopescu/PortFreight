namespace PortFreight.Application.DTOs.CustomerDTO;

public record CreateCustomerDto(
    string Name,
    string Email,
    string Phone
);