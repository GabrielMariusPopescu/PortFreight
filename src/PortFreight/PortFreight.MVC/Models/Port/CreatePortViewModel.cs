namespace PortFreight.MVC.Models.Port;

public class CreatePortViewModel
{
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "United Nations Code for Trade and Transport Locations")]
    public string UNLocode { get; set; }

    [Display(Name = "Country")]
    public string Country { get; set; }
}