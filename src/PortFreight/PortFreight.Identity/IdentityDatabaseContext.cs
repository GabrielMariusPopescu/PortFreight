namespace PortFreight.Identity;

public class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) 
    : IdentityDbContext<User, Role, string>(options)
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }
}
