﻿namespace AutoLot.Models.Entities.Configuration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasQueryFilter(c => c.IsDrivable);

        builder.Property(p => p.IsDrivable)
            .HasField("_isDrivable")
            .HasDefaultValue(true);

        builder.Property(e => e.DateBuilt).HasDefaultValueSql("GetDate()");

        builder.Property(e => e.Display)
            .HasComputedColumnSql("[PetName] + ' (' + [Color] + ')'", stored: true);

        CultureInfo provider = new("en-US");
        NumberStyles style = NumberStyles.Currency | NumberStyles.AllowCurrencySymbol;
        builder.Property(p => p.Price)
            .HasConversion(
            v => decimal.Parse(v, style, provider),
            v => v.ToString("C2"));

        builder.HasOne(d => d.MakeNavigation)
            .WithMany(p => p.Cars)
            .HasForeignKey(d => d.MakeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Inventory_Makes_MakeId");

        builder
            .HasMany(p => p.Drivers)
            .WithMany(p => p.Cars)
            .UsingEntity<CarDriver>(
                j => j
                .HasOne(cd => cd.DriverNavigation)
                .WithMany(d => d.CarDrivers)
                .HasForeignKey(nameof(CarDriver.DriverId))
                .HasConstraintName("FK_InventoryDriver_Drivers_DriverId")
                .OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasOne(cd => cd.CarNavigation)
                .WithMany(c => c.CarDrivers)
                .HasForeignKey(nameof(CarDriver.CarId))
                .HasConstraintName("FK_InventoryDriver_Inventory_InventoryId")
                .OnDelete(DeleteBehavior.ClientCascade),
                j =>
                {
                    j.HasKey(cd => new { cd.CarId, cd.DriverId });
                });

        builder.ToTable(b => b.IsTemporal(t =>
        {
            t.HasPeriodEnd("ValidTo");
            t.HasPeriodStart("ValidFrom");
            t.UseHistoryTable("InventoryAudit");
        }));

    }
}
