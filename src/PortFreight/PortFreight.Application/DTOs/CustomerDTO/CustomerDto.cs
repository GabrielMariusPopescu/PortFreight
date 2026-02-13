namespace PortFreight.Application.DTOs.CustomerDTO;

public record CustomerDto(
    Guid Id,
    string Name,
    string Email,
    string Phone
);