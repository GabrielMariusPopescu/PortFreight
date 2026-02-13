namespace PortFreight.Application.DTOs.PortDTO;

public record UpdatePortDto(
    Guid Id,
    string Name,
    string UNLocode,
    string Country
);
