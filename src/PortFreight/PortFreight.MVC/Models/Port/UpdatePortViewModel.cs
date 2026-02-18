namespace PortFreight.MVC.Models.Port;

[Authorize(Roles = "Administrator")]
public class UpdatePortViewModel
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "United Nations Code for Trade and Transport Locations")]
    public string UNLocode { get; set; }

    [Display(Name = "Country")]
    public string Country { get; set; }
}