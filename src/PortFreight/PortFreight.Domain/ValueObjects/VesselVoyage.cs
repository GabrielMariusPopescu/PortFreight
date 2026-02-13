namespace PortFreight.Domain.ValueObjects;

public class VesselVoyage
{
    public Guid Id { get; set; }
    public Guid VesselId { get; set; }
    public Vessel Vessel { get; set; } = default!;

    public string VoyageNumber { get; set; } = default!;

    public Guid DeparturePortId { get; set; }
    public Port DeparturePort { get; set; } = default!;

    public Guid ArrivalPortId { get; set; }
    public Port ArrivalPort { get; set; } = default!;

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
