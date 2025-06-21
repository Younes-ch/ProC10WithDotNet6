namespace AutoLot.Samples;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        SavingChanges += (sender, args) =>
        {
            Console.WriteLine($"Saving changes for {((DbContext)sender).Database.
            GetConnectionString()}");
        };

        SavedChanges += (sender, args) =>
        {
            Console.WriteLine($"Saved {args.EntitiesSavedCount} entities");
        };

        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
        };

        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        ChangeTracker.Tracked += ChangeTracker_Tracked;
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Make> Makes { get; set; }
    public DbSet<Radio> Radios { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Car>(entity =>
        //{
        //    entity.HasOne(d => d.MakeNavigation)
        //    .WithMany(p => p.Cars)
        //    .HasForeignKey(d => d.MakeId)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_Inventory_Makes_MakeId");
        //});
    }

    private void ChangeTracker_Tracked(object sender, EntityTrackedEventArgs e)
    {
        if (e.FromQuery)
        {
            Console.WriteLine($"An entity of type {e.Entry.Entity.GetType().Name} was loaded from the database.");

        }
    }

    private void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
    {
        if (e.OldState == EntityState.Modified && e.NewState == EntityState.Unchanged)
        {
            Console.WriteLine($"An entity of type {e.Entry.Entity.GetType().Name} was updated.");
        }
    }
}
