namespace PortFreight.Data.Entities.Operations;

public class GateMovement
{
    public Guid Id { get; set; }

    public Truck Truck { get; set; }

    public DateTime TimeIn { get; set; }

    public DateTime TimeOut { get; set; }
}