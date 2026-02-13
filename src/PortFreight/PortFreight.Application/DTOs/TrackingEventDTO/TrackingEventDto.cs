namespace PortFreight.Application.DTOs.TrackingEventDTO;

public record TrackingEventDto(
    Guid Id,
    string EventType,
    DateTime Timestamp,
    string Location,
    Guid ShipmentId
);
