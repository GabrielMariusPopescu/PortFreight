namespace PortFreight.Data.Entities.Compliance;

public class Inspection
{
    public Guid Id { get; set; }

    public InspectionType InspectionType { get; set; }

    public string Result { get; set; }

    public string Officer { get; set; }

    public DateTime Timestamp { get; set; }
}