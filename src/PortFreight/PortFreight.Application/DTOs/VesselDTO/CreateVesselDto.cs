namespace PortFreight.Application.DTOs.VesselDTO;

public record CreateVesselDto(
    string Name,
    string IMO,
    int CapacityTEU
);