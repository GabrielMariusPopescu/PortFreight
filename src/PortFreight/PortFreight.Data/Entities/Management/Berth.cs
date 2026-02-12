namespace PortFreight.Data.Entities.Management;

public class Berth
{
    public Guid Id { get; set; }

    public decimal Length { get; set; }

    public decimal Depth { get; set; }

    public required string Availability { get; set; }

    public Schedule Schedule { get; set; }
}