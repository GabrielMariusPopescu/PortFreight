namespace PortFreight.MVC.Models.Auth;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; init; } = string.Empty;

    [Required]
    [Display(Name = "Username")]
    public string Username { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}