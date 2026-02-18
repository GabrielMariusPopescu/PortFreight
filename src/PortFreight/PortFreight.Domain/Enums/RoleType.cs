namespace PortFreight.Domain.Enums;

public enum RoleType
{
    [Display(Name = "Administrator")]
    Administrator,

    [Display(Name = "User")]
    User,

    [Display(Name = "Manager")]
    Manager,

    [Display(Name = "Unknown")]
    Unknown
}