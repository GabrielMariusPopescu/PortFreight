namespace PortFreight.MVC.Models.Container;

public class CreateContainerViewModel
{
    [Display(Name = "Number")]
    public string ContainerNumber { get; set; }

    [Display(Name = "Type")]
    public string Type { get; set; }

    [Display(Name = "Weight")]
    public decimal Weight { get; set; }
}