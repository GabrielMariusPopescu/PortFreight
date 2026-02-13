namespace PortFreight.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
