namespace PortFreight.Application.Mappings;

public static class ShipmentMapping
{
    public static ShipmentDto ToDto(this Shipment shipment) =>
        new(
            shipment.Id,
            shipment.ReferenceNumber,
            shipment.CustomerId,
            shipment.OriginPortId,
            shipment.DestinationPortId,
            shipment.VesselVoyageId,
            shipment.Status,
            shipment.CreatedAt
        );

    public static Shipment ToEntity(this CreateShipmentDto dto) =>
        new()
        {
            ReferenceNumber = dto.ReferenceNumber,
            CustomerId = dto.CustomerId,
            OriginPortId = dto.OriginPortId,
            DestinationPortId = dto.DestinationPortId,
            VesselVoyageId = dto.VesselVoyageId,
            Status = ShipmentStatus.Booked,
            CreatedAt = DateTime.UtcNow
        };
}
