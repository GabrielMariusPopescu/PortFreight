namespace PortFreight.MVC.Models.User;

public class UserViewModel
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Is Email Confirmed?")]
    public bool IsEmailConfirmed { get; set; }

    [Display(Name = "Username")]
    public string Username { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Is Phone Number Confirmed?")]
    public bool IsPhoneNumberConfirmed { get; set; }
}