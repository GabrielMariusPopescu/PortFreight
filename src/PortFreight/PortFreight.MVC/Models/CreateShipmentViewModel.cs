namespace PortFreight.MVC.Models;

public class CreateShipmentViewModel
{
    [Required]
    public string ReferenceNumber { get; set; } = string.Empty;

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid OriginPortId { get; set; }

    [Required]
    public Guid DestinationPortId { get; set; }

    [Required]
    public Guid VesselVoyageId { get; set; }

    [Required]
    public ShipmentStatus Status { get; set; }
}