namespace PortFreight.MVC.Models.Shipment;

public class UpdateShipmentViewModel
{
    [Required] 
    public ShipmentStatus Status { get; set; }
}