namespace PortFreight.Data.Entities.Compliance;

public class CustomDeclaration
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public List<string> Duties { get; set; }

    public string InspectionStatus { get; set; }
}