namespace PortFreight.Domain.Entities;

public class Vessel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string IMO { get; set; } = default!;
    public int CapacityTEU { get; set; }

    public ICollection<VesselVoyage> Voyages { get; set; } = new List<VesselVoyage>();
}
