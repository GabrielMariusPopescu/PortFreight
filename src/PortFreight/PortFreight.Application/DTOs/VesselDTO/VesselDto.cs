namespace PortFreight.Application.DTOs.VesselDTO;

public record VesselDto(
    Guid Id,
    string Name,
    string IMO,
    int CapacityTEU
);

