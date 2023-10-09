using Carcard.Database.Entities;
using Carcard.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carcard.Database.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<OwnerEntity> Owners { get; set; }
    public DbSet<VehicleEntity> Vehicles { get; set; }
    public DbSet<MaintenanceEntity> Maintenances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntity<OwnerEntity>(modelBuilder, e =>
        {
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Name).IsRequired();
            e.HasMany(x => x.Vehicles).WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId);
        });

        ConfigureEntity<VehicleEntity>(modelBuilder, e =>
        {
            e.HasMany(x => x.Maintenances).WithOne(x => x.Vehicle)
                .HasForeignKey(x => x.VehicleId);
        });

        ConfigureEntity<MaintenanceEntity>(modelBuilder, e =>
        {
            e.Property(x => x.Repairman).IsRequired();
            e.Property(x => x.Distance).IsRequired();
            e.Property(x => x.Description).IsRequired();
            e.Property(x => x.Cost).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureEntity<T>(ModelBuilder mb, Action<EntityTypeBuilder<T>>? customConfiguration = null, bool customKey = false) where T : BaseEntity
    {
        var name = typeof(T).Name.Replace("Entity", null);
        mb.Entity<T>(e =>
        {
            e.ToTable(name);
            if (!customKey)
            {
                e.HasKey(x => x.Id);
            }
            e.HasQueryFilter(x => x.EntityStatus == eEntityStatus.Active);
            customConfiguration?.Invoke(e);
        });
    }
}
