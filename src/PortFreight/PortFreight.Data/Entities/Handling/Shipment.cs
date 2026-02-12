namespace PortFreight.Data.Entities.Handling;

public class Shipment
{
    public Guid Id { get; set; }

    public required string Shipper { get; set; }

    public required string Consignee { get; set; }

    public required string BillOfLanding { get; set; }

    public required string Terms { get; set; }
}