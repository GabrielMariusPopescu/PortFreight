namespace PortFreight.Application.Services.Implementation;

public class TrackingEventService : ITrackingEventService
{
    private readonly IGenericRepository<TrackingEvent> _repository;

    public TrackingEventService(IGenericRepository<TrackingEvent> repository)
    {
        _repository = repository;
    }

    public async Task<TrackingEvent?> GetTrackingEventAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<TrackingEvent>> GetTrackingEventsForShipmentAsync(Guid shipmentId)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(t => t.ShipmentId == shipmentId);
    }

    public async Task<TrackingEvent> AddTrackingEventAsync(TrackingEvent trackingEvent)
    {
        trackingEvent.Timestamp = DateTime.UtcNow;

        await _repository.AddAsync(trackingEvent);
        await _repository.SaveChangesAsync();

        return trackingEvent;
    }
}
