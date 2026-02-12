namespace PortFreight.Data.Entities.Management;

public class Voyage
{
    public Guid Id { get; set; }

    public DateOnly EstimatedTimeOfArrival { get; set; }

    public DateOnly EstimatedTimeOfDeparture { get; set; }

    public required string Origin { get; set; }

    public required string Destination { get; set; }

    public required string Status { get; set; }

    public Vessel Vessel { get; set; }
}