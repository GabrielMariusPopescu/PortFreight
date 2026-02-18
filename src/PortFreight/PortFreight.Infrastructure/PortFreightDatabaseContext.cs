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

        // Seed Ports
        modelBuilder.Entity<Port>().HasData(
            new Port 
            { 
                // origin port
                Id = Guid.Parse("5E423B0C-879D-4612-A690-E9BF5DE4CE41"), 
                Name = "Port of Belfast", 
                UNLocode = "GBBEL", 
                Country = "United Kingdom" 
            },
            new Port 
            { 
                // destination port
                Id = Guid.Parse("0BB7BBE2-3BDB-4298-82F0-B921A79C1AF5"), 
                Name = "Port of Rotterdam", 
                UNLocode = "NLRTM", 
                Country = "Netherlands" 
            }
        );

        // Seed Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer 
            { 
                Id = Guid.Parse("6D283DE6-B929-45CE-BEC4-087ADA6AA777"), 
                Name = "Acme Logistics", 
                Email = "info@acme.com", 
                Phone = "+44 1234 567890" 
            }
        );

        // Seed Vessels
        modelBuilder.Entity<Vessel>().HasData(
            new Vessel 
            {
                Id = Guid.Parse("28DE9FE0-CFBB-4DC1-BB44-23497B16C292"), 
                Name = "MV Horizon", 
                IMO = "9876543", 
                CapacityTEU = 12000 
            }
        );

        // Seed Voyage
        modelBuilder.Entity<VesselVoyage>().HasData(
            new VesselVoyage
            {
                Id = Guid.Parse("F905BCBB-1101-46A9-A503-AA01212C72FB"),
                VesselId = Guid.Parse("28DE9FE0-CFBB-4DC1-BB44-23497B16C292"),
                VoyageNumber = "HZ123",
                DeparturePortId = Guid.Parse("5E423B0C-879D-4612-A690-E9BF5DE4CE41"),
                ArrivalPortId = Guid.Parse("0BB7BBE2-3BDB-4298-82F0-B921A79C1AF5"),
                DepartureTime = new DateTime(2026,2,11,13,00,00), //DateTime.UtcNow.AddDays(-2),
                ArrivalTime = new DateTime(2026,2,10,13,00,00) //DateTime.UtcNow.AddDays(3)
            }
        );

        // Seed Shipment
        modelBuilder.Entity<Shipment>().HasData(
            new Shipment
            {
                Id = Guid.Parse("E14CF002-1FAE-4A03-9D9A-75F20F9631A1"),
                ReferenceNumber = "SHIP-0001",
                CustomerId = Guid.Parse("6D283DE6-B929-45CE-BEC4-087ADA6AA777"),
                OriginPortId = Guid.Parse("5E423B0C-879D-4612-A690-E9BF5DE4CE41"),
                DestinationPortId = Guid.Parse("0BB7BBE2-3BDB-4298-82F0-B921A79C1AF5"),
                VesselVoyageId = Guid.Parse("F905BCBB-1101-46A9-A503-AA01212C72FB"),
                Status = ShipmentStatus.InTransit,
                CreatedAt = new DateTime(2026,2,12,13,00,00) //DateTime.UtcNow.AddDays(-1)
            }
        );

        // Seed Container
        modelBuilder.Entity<Container>().HasData(
            new Container
            {
                Id = Guid.Parse("09F83D1A-0B7B-42D5-B830-F6051BE5DB7E"),
                ContainerNumber = "CONT1234567",
                Type = "40ft",
                Weight = 12000,
                ShipmentId = Guid.Parse("E14CF002-1FAE-4A03-9D9A-75F20F9631A1")
            }
        );
    }
}