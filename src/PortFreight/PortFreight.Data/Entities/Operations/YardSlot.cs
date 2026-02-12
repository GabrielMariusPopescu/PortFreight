namespace PortFreight.Data.Entities.Operations;

public class YardSlot
{
    public Guid Id { get; set; }

    public string Location { get; set; }

    public bool IsAvailable { get; set; }
}