namespace PortFreight.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var administrator = new User
        {
            Id = Guid.Parse("7DD72F8C-0FDC-4B63-8307-1C31E81515D2").ToString(),
            FirstName = "Jon",
            LastName = "Doe",
            Email = "administrator@portfreight.co.uk",
            NormalizedEmail = "ADMNISTRATOR@PORTFREIGHT.CO.UK",
            UserName = "Jon Doe",
            NormalizedUserName = "JON DOE"
        };
        administrator.PasswordHash = HashPassword(administrator, "P@ssword123!");

        var manager = new User
        {
            Id = Guid.Parse("A59C7E4B-4365-4548-94DA-F620B7EDBF37").ToString(),
            FirstName = "Bob",
            LastName = "Johnson",
            Email = "manager@portfreight.co.uk",
            NormalizedEmail = "MANAGER@PORTFREIGHT.CO.UK",
            UserName = "Bob Johnson",
            NormalizedUserName = "BOB JOHNSON"
        };
        manager.PasswordHash = HashPassword(manager, "P@ssword123!");

        var user = new User
        {
            Id = Guid.Parse("63C9640E-6560-4F5D-AC14-ADF468A067B0").ToString(),
            FirstName = "Jane",
            LastName = "Smith",
            Email = "user@portfreight.co.uk",
            NormalizedEmail = "USER@PORTFREIGHT.CO.UK",
            UserName = "Jane Smith",
            NormalizedUserName = "JANE SMITH"
        };
        user.PasswordHash = HashPassword(user, "P@ssword123!");

        var unknown = new User
        {
            Id = Guid.Parse("37B650FA-1635-4720-A61D-BC72C48DB563").ToString(),
            FirstName = "Alice",
            LastName = "Williams",
            Email = "unknown@portfreight.co.uk",
            NormalizedEmail = "UNKNOWN@PORTFREIGHT.CO.UK",
            UserName = "Alice Williams",
            NormalizedUserName = "ALICE WILLIAMS"
        };

        unknown.PasswordHash = HashPassword(unknown, "P@ssword123!");

        builder.HasData(administrator, manager, user, unknown);
    }

    private static string HashPassword(User user, string password)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.HashPassword(user, password);
    }
}