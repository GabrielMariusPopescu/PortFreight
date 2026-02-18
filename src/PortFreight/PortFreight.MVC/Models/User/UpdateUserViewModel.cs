namespace PortFreight.MVC.Models.User;

public class UpdateUserViewModel
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Username")]
    public string Username { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}