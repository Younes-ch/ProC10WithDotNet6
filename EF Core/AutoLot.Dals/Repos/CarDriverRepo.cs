﻿namespace AutoLot.Dals.Repos;

public class CarDriverRepo : TemporalTableBaseRepo<CarDriver>, ICarDriverRepo
{
    public CarDriverRepo(ApplicationDbContext context) : base(context)
    {
    }

    internal CarDriverRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    internal IIncludableQueryable<CarDriver, Driver> BuildBaseQuery()
        => Table.Include(c => c.CarNavigation).Include(d => d.DriverNavigation);

    public override IEnumerable<CarDriver> GetAll()
        => BuildBaseQuery();

    public override IEnumerable<CarDriver> GetAllIgnoreQueryFilters()
        => BuildBaseQuery().IgnoreQueryFilters();

    public override CarDriver Find(int? id)
        => BuildBaseQuery().IgnoreQueryFilters().Where(x => x.Id == id).FirstOrDefault();
}
