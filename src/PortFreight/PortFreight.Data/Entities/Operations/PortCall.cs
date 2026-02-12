namespace PortFreight.Data.Entities.Operations;

public class PortCall
{
    public Guid Id { get; set; }

    public List<ServiceRequest> ServicesRequested { get; set; }

    public DateTime Timestamp { get; set; }
}