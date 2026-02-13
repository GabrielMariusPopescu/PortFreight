namespace PortFreight.Domain.Entities;

public class Shipment
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } = default!;

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    public Guid OriginPortId { get; set; }
    public Port OriginPort { get; set; } = default!;

    public Guid DestinationPortId { get; set; }
    public Port DestinationPort { get; set; } = default!;

    public Guid VesselVoyageId { get; set; }
    public VesselVoyage VesselVoyage { get; set; } = default!;

    public ShipmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Container> Containers { get; set; } = new List<Container>();
    public ICollection<TrackingEvent> TrackingEvents { get; set; } = new List<TrackingEvent>();
}
