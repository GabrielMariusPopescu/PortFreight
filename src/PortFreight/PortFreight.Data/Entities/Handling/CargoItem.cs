namespace PortFreight.Data.Entities.Handling;

public class CargoItem
{
    public Guid Id { get; set; }

    public CargoType CargoType { get; set; }

    public decimal Weight { get; set; }

    public bool IsHazardous { get; set; }
}