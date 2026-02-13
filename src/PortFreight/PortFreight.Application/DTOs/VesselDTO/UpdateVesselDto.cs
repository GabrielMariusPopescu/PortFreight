namespace PortFreight.Application.DTOs.VesselDTO;

public record UpdateVesselDto(
    Guid Id,
    string Name,
    string IMO,
    int CapacityTEU
);
