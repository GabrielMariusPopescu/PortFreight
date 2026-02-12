namespace PortFreight.Data.Entities.Handling;

public class Container
{
    public Guid Id { get; set; }

    public decimal Width { get; set; }

    public decimal Length { get; set; }

    public decimal Height { get; set; }

    public int SealNumber { get; set; }

    public required string Status { get; set; }
}