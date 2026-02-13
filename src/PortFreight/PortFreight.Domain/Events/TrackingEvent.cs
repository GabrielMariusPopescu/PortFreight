namespace PortFreight.Domain.Events;

public class TrackingEvent
{
    public Guid Id { get; set; }
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; } = default!;

    public string EventType { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string Location { get; set; } = default!;
}
