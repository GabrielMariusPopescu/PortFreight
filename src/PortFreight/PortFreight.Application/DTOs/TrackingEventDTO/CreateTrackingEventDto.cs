namespace PortFreight.Application.DTOs.TrackingEventDTO;

public record CreateTrackingEventDto(
    string EventType,
    string Location
);