namespace PortFreight.MVC.Models;

public class UpdateShipmentViewModel
{
    [Required] 
    public ShipmentStatus Status { get; set; }
}