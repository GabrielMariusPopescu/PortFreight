namespace PortFreight.Data.Entities.Handling;

public class Equipment
{
    public Guid CraneId { get; set; }

    public required string Type { get; set; }

    public decimal Capacity { get; set; }
}