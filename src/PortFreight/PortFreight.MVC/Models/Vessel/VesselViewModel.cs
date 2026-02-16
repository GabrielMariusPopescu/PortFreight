namespace PortFreight.MVC.Models.Vessel;

public class VesselViewModel
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }
    
    [Display(Name = "International Maritime Organization")]
    public string IMO { get; set; }
    
    [Display(Name = "Capacity (Twenty-foot Equivalent Unit)")]
    public int CapacityTEU { get; set; }
}