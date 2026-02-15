namespace PortFreight.MVC.Models;

public class ShipmentViewModel
{
    public Guid Id { get; set; }

    public string ReferenceNumber { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid OriginPortId { get; set; }

    public Guid DestinationPortId { get; set; }

    public Guid VesselVoyageId { get; set; }

    public ShipmentStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}