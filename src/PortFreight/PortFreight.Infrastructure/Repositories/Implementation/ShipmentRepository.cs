namespace PortFreight.Infrastructure.Repositories.Implementation;

public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
{
    public ShipmentRepository(PortFreightDatabaseContext context) : base(context) { }

    public async Task<Shipment?> GetShipmentWithDetailsAsync(Guid id)
    {
        return await _context.Shipments
            .Include(shipment => shipment.Customer)
            .Include(shipment => shipment.Containers)
            .Include(shipment => shipment.TrackingEvents)
            .Include(shipment => shipment.VesselVoyage)
                .ThenInclude(vesselVoyage => vesselVoyage.Vessel)
            .FirstOrDefaultAsync(shipment => shipment.Id == id);
    }
}

