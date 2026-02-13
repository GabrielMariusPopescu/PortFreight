namespace PortFreight.Application.DTOs.PortDTO;

public record PortDto(
    Guid Id,
    string Name,
    string UNLocode,
    string Country
);
