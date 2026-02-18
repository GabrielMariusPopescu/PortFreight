namespace PortFreight.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var administratorUserRole = new UserRole
        {
            UserId = Guid.Parse("7DD72F8C-0FDC-4B63-8307-1C31E81515D2").ToString(),
            RoleId = Guid.Parse("9F10C98C-C714-496A-AFA0-0BFB802E07E2").ToString()
        };

        var managerUserRole = new UserRole
        {
            UserId = Guid.Parse("A59C7E4B-4365-4548-94DA-F620B7EDBF37").ToString(),
            RoleId = Guid.Parse("688CE16F-B76D-40CF-89DB-85D2EEB7D473").ToString()
        };

        var userRole = new UserRole
        {
            UserId = Guid.Parse("63C9640E-6560-4F5D-AC14-ADF468A067B0").ToString(),
            RoleId = Guid.Parse("D59EC08B-3ED7-4164-A08D-E38DEE019BF6").ToString()
        };

        var unknownUserRole = new UserRole
        {
            UserId = Guid.Parse("37B650FA-1635-4720-A61D-BC72C48DB563").ToString(),
            RoleId = Guid.Parse("B142936E-DFA0-4D56-B36F-B8AA279D98ED").ToString()
        };

        builder.HasData(administratorUserRole, managerUserRole, userRole, unknownUserRole);
    }
}