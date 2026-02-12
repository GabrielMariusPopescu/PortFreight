namespace PortFreight.Data.Entities.Logistic;

public class Unit
{
    public Guid Id { get; set; }

    public UnitType UnitType { get; set; }

    public string Operator { get; set; }

    public decimal Capacity { get; set; }
}