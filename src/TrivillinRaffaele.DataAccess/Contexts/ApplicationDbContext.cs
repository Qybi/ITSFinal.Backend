using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.PortableExecutable;
using Cled.TrivillinRaffaeleEsame.DataAccess.Abstractions.Contexts;
using Cled.TrivillinRaffaeleEsame.Models;
using Cled.TrivillinRaffaeleEsame.Models.Entities;

namespace Cled.TrivillinRaffaeleEsame.DataAccess.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // cycling all entities flagged for creation/update and automatically setting updatedAt timestamp
        foreach (var item in ChangeTracker.Entries<Entity>().AsEnumerable())
            item.Entity.UpdatedAt = DateTime.UtcNow;

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            e.Property(x => x.Price).HasPrecision(19, 2);
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        });
    }
}
