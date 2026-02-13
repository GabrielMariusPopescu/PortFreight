namespace PortFreight.Domain.Entities;

public class Container
{
    public Guid Id { get; set; }
    public string ContainerNumber { get; set; } = default!;
    public string Type { get; set; } = default!;
    public decimal Weight { get; set; }

    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; } = default!;

    public ICollection<ContainerStatusEvent> StatusEvents { get; set; } = new List<ContainerStatusEvent>();
}
