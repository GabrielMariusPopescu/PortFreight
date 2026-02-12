namespace PortFreight.Data.Entities.Logistic;

public class Order
{
    public Guid Id { get; set; }

    public Location PickupLocation { get; set; }

    public Location DeliveryLocation { get; set; }

    public Schedule Schedule { get; set; }
}