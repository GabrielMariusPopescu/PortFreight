namespace PortFreight.Data.Entities.Logistic;

public class Warehouse
{
    public Guid Id { get; set; }

    public decimal Capacity { get; set; }

    public bool HasTemperatureControl { get; set; }
}