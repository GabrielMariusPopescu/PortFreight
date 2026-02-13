namespace PortFreight.Application.DTOs.ShipmentDTO;

public record ShipmentDto(
    Guid Id,
    string ReferenceNumber,
    Guid CustomerId,
    Guid OriginPortId,
    Guid DestinationPortId,
    Guid VesselVoyageId,
    ShipmentStatus Status,
    DateTime CreatedAt
);
