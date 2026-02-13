namespace PortFreight.Application.Mappings;

public static class TrackingMapping
{
    public static TrackingEventDto ToDto(this TrackingEvent trackingEvent) =>
        new(trackingEvent.Id, trackingEvent.EventType, trackingEvent.Timestamp, trackingEvent.Location, trackingEvent.ShipmentId);

    public static TrackingEvent ToEntity(this CreateTrackingEventDto dto, Guid shipmentId) =>
        new()
        {
            EventType = dto.EventType,
            Location = dto.Location,
            ShipmentId = shipmentId,
            Timestamp = DateTime.UtcNow
        };
}
