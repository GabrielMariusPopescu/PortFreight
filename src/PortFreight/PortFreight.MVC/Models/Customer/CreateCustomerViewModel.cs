namespace PortFreight.MVC.Models.Customer;

public class CreateCustomerViewModel
{
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Phone")]
    public string Phone { get; set; }
}