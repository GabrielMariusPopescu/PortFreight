namespace PortFreight.Application.DTOs.ContainerDTO;

public record ContainerDto(
    Guid Id,
    string ContainerNumber,
    string Type,
    decimal Weight,
    Guid ShipmentId
);