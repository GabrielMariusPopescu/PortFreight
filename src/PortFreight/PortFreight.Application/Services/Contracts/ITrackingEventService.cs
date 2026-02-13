namespace PortFreight.Application.Services.Contracts;

public interface ITrackingEventService
{
    Task<TrackingEvent?> GetTrackingEventAsync(Guid id);
    Task<IEnumerable<TrackingEvent>> GetTrackingEventsForShipmentAsync(Guid shipmentId);
    Task<TrackingEvent> AddTrackingEventAsync(TrackingEvent trackingEvent);
}
