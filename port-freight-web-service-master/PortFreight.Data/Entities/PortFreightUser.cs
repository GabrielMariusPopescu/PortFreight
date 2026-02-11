using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PortFreight.Data.Entities;

public class PortFreightUser : IdentityUser
{
    [Required]
    public string SenderId { get; set; }

    [Required]
    public override string Email { get; set; }

    [Required]
    public override string UserName { get; set; }

    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

}   