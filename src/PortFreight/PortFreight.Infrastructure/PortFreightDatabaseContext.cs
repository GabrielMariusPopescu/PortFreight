namespace PortFreight.Infrastructure;

public class PortFreightDatabaseContext(DbContextOptions<PortFreightDatabaseContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Port> Ports => Set<Port>();
    public DbSet<Vessel> Vessels => Set<Vessel>();
    public DbSet<VesselVoyage> VesselVoyages => Set<VesselVoyage>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<Container> Containers => Set<Container>();
    public DbSet<ContainerStatusEvent> ContainerStatusEvents => Set<ContainerStatusEvent>();
    public DbSet<TrackingEvent> TrackingEvents => Set<TrackingEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Shipment → OriginPort
        modelBuilder.Entity<Shipment>()
       .HasOne(shipment => shipment.OriginPort)
       .WithMany()
       .HasForeignKey(shipment => shipment.OriginPortId)
       .OnDelete(DeleteBehavior.Restrict);

        // Shipment → DestinationPort
        modelBuilder.Entity<Shipment>()
            .HasOne(shipment => shipment.DestinationPort)
            .WithMany()
            .HasForeignKey(shipment => shipment.DestinationPortId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer → Shipments
        modelBuilder.Entity<Customer>()
            .HasMany(customer => customer.Shipments)
            .WithOne(shipment => shipment.Customer)
            .HasForeignKey(shipment => shipment.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Port → VesselVoyages (Departure)
        modelBuilder.Entity<Port>()
            .HasMany(port => port.DepartingVoyages)
            .WithOne(vesselVoyage => vesselVoyage.DeparturePort)
            .HasForeignKey(vesselVoyage => vesselVoyage.DeparturePortId)
            .OnDelete(DeleteBehavior.Restrict);

        // Port → VesselVoyages (Arrival)
        modelBuilder.Entity<Port>()
            .HasMany(port => port.ArrivingVoyages)
            .WithOne(vesselVoyage => vesselVoyage.ArrivalPort)
            .HasForeignKey(vesselVoyage => vesselVoyage.ArrivalPortId)
            .OnDelete(DeleteBehavior.Restrict);

        // Vessel → Voyages
        modelBuilder.Entity<Vessel>()
            .HasMany(vessel => vessel.Voyages)
            .WithOne(vesselVoyage => vesselVoyage.Vessel)
            .HasForeignKey(vesselVoyage => vesselVoyage.VesselId)
            .OnDelete(DeleteBehavior.Restrict);

        // VesselVoyage → Shipments
        modelBuilder.Entity<VesselVoyage>()
            .HasMany(vesselVoyage => vesselVoyage.Shipments)
            .WithOne(shipment => shipment.VesselVoyage)
            .HasForeignKey(shipment => shipment.VesselVoyageId)
            .OnDelete(DeleteBehavior.Restrict);

        // Shipment → Containers
        modelBuilder.Entity<Shipment>()
            .HasMany(shipment => shipment.Containers)
            .WithOne(container => container.Shipment)
            .HasForeignKey(container => container.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Shipment → TrackingEvents
        modelBuilder.Entity<Shipment>()
            .HasMany(shipment => shipment.TrackingEvents)
            .WithOne(trackingEvent => trackingEvent.Shipment)
            .HasForeignKey(trackingEvent => trackingEvent.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Container → StatusEvents
        modelBuilder.Entity<Container>()
            .HasMany(container => container.StatusEvents)
            .WithOne(containerStatusEvent => containerStatusEvent.Container)
            .HasForeignKey(containerStatusEvent => containerStatusEvent.ContainerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure decimal precision for Container.Weight
        modelBuilder.Entity<Container>()
            .Property(c => c.Weight)
            .HasPrecision(18, 4);
    }
}