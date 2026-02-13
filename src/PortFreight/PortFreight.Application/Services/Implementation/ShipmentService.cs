namespace PortFreight.Application.Services.Implementation;


public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IGenericRepository<Container> _containerRepository;
    private readonly IGenericRepository<TrackingEvent> _trackingRepository;

    public ShipmentService(
        IShipmentRepository shipmentRepository,
        IGenericRepository<Container> containerRepository,
        IGenericRepository<TrackingEvent> trackingRepository)
    {
        _shipmentRepository = shipmentRepository;
        _containerRepository = containerRepository;
        _trackingRepository = trackingRepository;
    }

    public async Task<Shipment?> GetShipmentAsync(Guid id)
    {
        return await _shipmentRepository.GetShipmentWithDetailsAsync(id);
    }

    public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
    {
        return await _shipmentRepository.GetAllAsync();
    }

    public async Task<Shipment> CreateShipmentAsync(Shipment shipment)
    {
        await _shipmentRepository.AddAsync(shipment);
        await _shipmentRepository.SaveChangesAsync();
        return shipment;
    }

    public async Task<bool> UpdateShipmentStatusAsync(Guid shipmentId, ShipmentStatus newStatus)
    {
        var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);
        if (shipment == null)
            return false;

        shipment.Status = newStatus;
        _shipmentRepository.Update(shipment);
        await _shipmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddContainerToShipmentAsync(Guid shipmentId, Container container)
    {
        var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);
        if (shipment == null)
            return false;

        container.ShipmentId = shipmentId;

        await _containerRepository.AddAsync(container);
        await _containerRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTrackingEventAsync(Guid shipmentId, TrackingEvent trackingEvent)
    {
        var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);
        if (shipment == null)
            return false;

        trackingEvent.ShipmentId = shipmentId;
        trackingEvent.Timestamp = DateTime.UtcNow;

        await _trackingRepository.AddAsync(trackingEvent);
        await _trackingRepository.SaveChangesAsync();

        return true;
    }
}
