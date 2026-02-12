namespace PortFreight.Data.Entities;

public class Location
{
    [StringLength(100)]
    public required string LineAddress { get; set; }

    [StringLength(50)]
    public required string City { get; set; }

    [StringLength(50)]
    public required string County { get; set; }

    [Required(ErrorMessage = "Enter a post code")]
    [StringLength(10, MinimumLength = 5, ErrorMessage = "Enter a valid post code")]
    public required string Postcode { get; set; }

    [StringLength(50)]
    public required string Country { get; set; }
}