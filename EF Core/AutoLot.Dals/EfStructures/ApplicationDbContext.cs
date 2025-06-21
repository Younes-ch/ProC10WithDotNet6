namespace AutoLot.Dals.EfStructures;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        SavingChanges += (sender, args) =>
        {
            string cs = ((ApplicationDbContext)sender).Database!.GetConnectionString();
            Console.WriteLine($"Saving changes for {cs}");
        };

        SavedChanges += (sender, args) =>
        {
            string cs = ((ApplicationDbContext)sender).Database!.GetConnectionString();
            Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {cs}");
        };

        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
        };

        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    private void ChangeTracker_Tracked(object sender, EntityTrackedEventArgs e)
    {
        var source = (e.FromQuery) ? "Database" : "Code";

        if (e.Entry.Entity is Car c)
        {
            Console.WriteLine($"Car entry {c.PetName} was added from {source}");
        }
    }

    private void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
    {
        if (e.Entry.Entity is not Car c)
        {
            return;
        }

        var action = string.Empty;
        Console.WriteLine($"Car {c.PetName} was {e.OldState} before the state changed to {e.NewState}");

        switch (e.NewState)
        {
            case EntityState.Unchanged:
                action = e.OldState switch
                {
                    EntityState.Added => "Added",
                    EntityState.Modified => "Edited",
                    _ => action
                };
                Console.WriteLine($"The object was {action}");
                break;
        }
    }

    public virtual DbSet<CreditRisk> CreditRisks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerOrderViewModel> CustomerOrderViews { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<CarDriver> CarsToDrivers { get; set; }

    public virtual DbSet<Radio> Radios { get; set; }

    public virtual DbSet<CustomerOrderViewModel> CustomerOrderViewModels { get; set; }

    public virtual DbSet<SeriLogEntry> SeriLogEntries { get; set; }

    [DbFunction("udf_CountMakes", Schema = "dbo")]
    public static int InventoryCountFor(int makeId) => throw new NotSupportedException();

    [DbFunction("udf_GetCarsForMake", Schema = "dbo")]
    public IQueryable<Car> GetCarsFor(int makeId) => FromExpression(() => GetCarsFor(makeId));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new DriverConfiguration());
        modelBuilder.ApplyConfiguration(new CarDriverConfiguration());
        modelBuilder.ApplyConfiguration(new RadioConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new MakeConfiguration());
        modelBuilder.ApplyConfiguration(new CreditRiskConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerOrderViewModelConfiguration());
        modelBuilder.ApplyConfiguration(new SeriLogEntryConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(50);
        configurationBuilder.IgnoreAny<INonPersisted>();
    }

    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new CustomConcurrencyException("A concurrency error happened.", ex);
        }
        catch (RetryLimitExceededException ex)
        {
            throw new CustomRetryLimitExceededException("There is a problem with SQL Server.", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new CustomDbUpdateException("An error occurred updating the database.", ex);
        }
        catch (Exception ex)
        {
            throw new CustomException("An error occurred updating the database.", ex);
        }
    }
}
