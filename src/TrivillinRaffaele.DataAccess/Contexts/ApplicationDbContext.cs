using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrivillinRaffaele.DataAccess.Abstractions.Contexts;
using TrivillinRaffaele.Models;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.DataAccess.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<SensorData> SensorsData { get; set; }

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
        modelBuilder.Entity<SensorData>(e =>
        {
            e.HasOne(x => x.Sensor)
                .WithMany(x => x.SensorData)
                .HasForeignKey(x => x.SensorId);

            e.Property(x => x.Magnitude).HasPrecision(5, 2);
            e.Property(x => x.Depth).HasPrecision(9, 4); // 0.1 mm precision
            e.Property(x => x.Latitude).HasPrecision(12, 8);
            e.Property(x => x.Longitude).HasPrecision(12, 8);
        });

        modelBuilder.Entity<Sensor>(e =>
        {
            e.HasMany(x => x.SensorData)
                .WithOne(x => x.Sensor)
                .HasForeignKey(x => x.SensorId);

            e.Property(x => x.Latitude).HasPrecision(12, 8);
            e.Property(x => x.Longitude).HasPrecision(12, 8);
        });
    }
}
