namespace PortFreight.Application.DTOs.PortDTO;

public record CreatePortDto(
    string Name,
    string UNLocode,
    string Country
);