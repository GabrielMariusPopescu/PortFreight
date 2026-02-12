namespace PortFreight.Identity.Entities;

public class User : IdentityUser
{
    [StringLength(30)]
    public required string FirstName { get; set; }

    [StringLength(30)]
    public required string LastName { get; set; }

    public Address? Address { get; set; }
}