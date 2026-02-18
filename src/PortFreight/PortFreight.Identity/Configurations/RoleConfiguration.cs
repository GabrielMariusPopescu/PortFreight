namespace PortFreight.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        var administrator = new Role
        {
            Id = Guid.Parse("9F10C98C-C714-496A-AFA0-0BFB802E07E2").ToString(),
            Name = RoleType.Administrator.GetDisplayName(),
            NormalizedName = RoleType.Administrator.GetDisplayName().ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var manager = new Role
        {
            Id = Guid.Parse("688CE16F-B76D-40CF-89DB-85D2EEB7D473").ToString(),
            Name = RoleType.Manager.GetDisplayName(),
            NormalizedName = RoleType.Manager.GetDisplayName().ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var user = new Role
        {
            Id = Guid.Parse("D59EC08B-3ED7-4164-A08D-E38DEE019BF6").ToString(),
            Name = RoleType.User.GetDisplayName(),
            NormalizedName = RoleType.User.GetDisplayName().ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var unknown = new Role
        {
            Id = Guid.Parse("B142936E-DFA0-4D56-B36F-B8AA279D98ED").ToString(),
            Name = RoleType.Unknown.GetDisplayName(),
            NormalizedName = RoleType.Unknown.GetDisplayName().ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        builder.HasData(administrator, manager, user, unknown);
    }
}