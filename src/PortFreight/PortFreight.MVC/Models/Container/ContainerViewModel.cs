namespace PortFreight.MVC.Models.Container;

public class ContainerViewModel
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Number")]
    public string ContainerNumber { get; set; }
    
    [Display(Name = "Type")]
    public string Type { get; set; }
    
    [Display(Name = "Weight")]
    public decimal Weight { get; set; }
    
    [Display(Name = "Shipment Id")]
    public Guid ShipmentId { get; set; }
}