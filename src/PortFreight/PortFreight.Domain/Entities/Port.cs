namespace PortFreight.Domain.Entities;

public class Port
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string UNLocode { get; set; } = default!;
    public string Country { get; set; } = default!;

    public ICollection<VesselVoyage> DepartingVoyages { get; set; } = new List<VesselVoyage>();
    public ICollection<VesselVoyage> ArrivingVoyages { get; set; } = new List<VesselVoyage>();
}
