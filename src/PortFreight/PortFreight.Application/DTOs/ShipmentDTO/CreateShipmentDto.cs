namespace PortFreight.Application.DTOs.ShipmentDTO;

public record CreateShipmentDto(
    string ReferenceNumber,
    Guid CustomerId,
    Guid OriginPortId,
    Guid DestinationPortId,
    Guid VesselVoyageId
);