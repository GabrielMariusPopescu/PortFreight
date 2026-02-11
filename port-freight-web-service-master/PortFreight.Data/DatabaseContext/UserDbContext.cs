using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortFreight.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortFreight.Data.DatabaseContext;

public class UserDbContext : IdentityDbContext<PortFreightUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<PreApprovedUser> PreApprovedUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PortFreightUser>(b =>
        {
            b.ToTable("PortFreightUsers");
        });

        modelBuilder.Entity<PortFreightUser>(b =>
        {
            b.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();
        });
    }
}
