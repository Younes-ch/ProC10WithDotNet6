namespace AutoLot.Dals.Repos;

public class CarRepo : TemporalTableBaseRepo<Car>, ICarRepo
{
    public CarRepo(ApplicationDbContext context) : base(context)
    {
    }

    internal CarRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    internal IOrderedQueryable<Car> BuildBaseQuery()
        => Table.Include(c => c.MakeNavigation).OrderBy(c => c.PetName);

    public override IEnumerable<Car> GetAll()
        => BuildBaseQuery();

    public override IEnumerable<Car> GetAllIgnoreQueryFilters()
        => BuildBaseQuery().IgnoreQueryFilters();

    public IEnumerable<Car> GetAllBy(int makeId)
        => BuildBaseQuery().Where(c => c.MakeId == makeId);

    public override Car Find(int? id)
        => Table
                .IgnoreQueryFilters()
                .Where(c => c.Id == id)
                .Include(c => c.MakeNavigation)
                .FirstOrDefault();

    public string GetPetName(int id)
    {
        var parameterId = new SqlParameter
        {
            ParameterName = "@carId",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
            Value = id
        };

        var parameterName = new SqlParameter
        {
            ParameterName = "@petName",
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Output
        };

        ExecuteParameterizedQuery("EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", [parameterId, parameterName]);

        return (string)parameterName.Value;
    }
}
