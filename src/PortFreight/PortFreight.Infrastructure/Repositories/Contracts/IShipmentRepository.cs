namespace PortFreight.Infrastructure.Repositories.Contracts;

public interface IShipmentRepository : IGenericRepository<Shipment>
{
    Task<Shipment?> GetShipmentWithDetailsAsync(Guid id);
}
