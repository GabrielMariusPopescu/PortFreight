using System.ComponentModel.DataAnnotations;

namespace PortFreight.Data.Entities;

public class ApiKey
{
    [Key]
    public string Id { get; set; }

    public string Token { get; set; }
    public string Source { get; set; }
}
