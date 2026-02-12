namespace PortFreight.Data.Entities.Compliance;

public class Document
{
    public Guid Id { get; set; }

    public string BillOfLanding { get; set; }

    public string Manifest { get; set; }

    public string Certificates { get; set; }
}