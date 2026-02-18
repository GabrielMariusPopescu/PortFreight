namespace PortFreight.Infrastructure.Repositories.Implementation;

public class ShipmentRepository(PortFreightDatabaseContext context)
    : GenericRepository<Shipment>(context), IShipmentRepository
{
    public async Task<Shipment?> GetShipmentWithDetailsAsync(Guid id)
    {
        return await Context.Shipments
            .Include(shipment => shipment.Customer)
            .Include(shipment => shipment.Containers)
            .Include(shipment => shipment.TrackingEvents)
            .Include(shipment => shipment.VesselVoyage)
                .ThenInclude(vesselVoyage => vesselVoyage.Vessel)
            .FirstOrDefaultAsync(shipment => shipment.Id == id);
    }
}

