namespace PortFreight.Data.Entities.Management;

public class Vessel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required int ImoNumber { get; set; }

    public required string Type { get; set; }

    public required decimal Capacity { get; set; }

    public required string Operator { get; set; }

    public List<Voyage> Voyages { get; set; }
}