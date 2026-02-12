namespace PortFreight.Data.Entities.Operations;

public class ServiceRequest
{
    public Guid Id { get; set; }

    public string Pilotage { get; set; }

    public string Towage { get; set; }

    public string Bunkering { get; set; }

    public string WasteDisposal { get; set; }
}