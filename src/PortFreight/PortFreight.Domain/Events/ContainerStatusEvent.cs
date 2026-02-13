namespace PortFreight.Domain.Events;

public class ContainerStatusEvent
{
    public Guid Id { get; set; }
    public Guid ContainerId { get; set; }
    public Container Container { get; set; } = default!;

    public string Status { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string Location { get; set; } = default!;
}
