namespace PortFreight.Identity.Models;

public class User : IdentityUser
{
    [StringLength(50)]
    public required string FirstName { get; init; }

    [StringLength(50)]
    public required string LastName { get; init; }
}
