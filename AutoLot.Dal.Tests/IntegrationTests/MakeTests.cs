namespace AutoLot.Dal.Tests.IntegrationTests;

[Collection("Integration Tests")]
public class MakeTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
{
    private readonly IMakeRepo _makeRepo;

    public MakeTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _makeRepo = new MakeRepo(Context);
    }

    [Fact]
    public void ShouldGetAllMakesAndCarsThatAreYellow()
    {
        var query = Context.Makes.IgnoreQueryFilters()
            .Include(x => x.Cars.Where(x => x.Color == "Yellow"));
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        var makes = query.ToList();
        Assert.NotNull(makes);
        Assert.NotEmpty(makes);
        Assert.NotEmpty(makes.Where(x => x.Cars.Any()));
        Assert.Empty(makes.First(m => m.Id == 1).Cars);
        Assert.Empty(makes.First(m => m.Id == 2).Cars);
        Assert.Empty(makes.First(m => m.Id == 3).Cars);
        Assert.Single(makes.First(m => m.Id == 4).Cars);
        Assert.Empty(makes.First(m => m.Id == 5).Cars);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    [InlineData(6, 1)]
    public void ShouldGetAllCarsForAMakeExplicitlyWithQueryFilters(int makeId, int carCount)
    {
        var make = Context.Makes.First(x => x.Id == makeId);
        IQueryable<Car> query = Context.Entry(make).Collection(m => m.Cars).Query();
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        query.Load();
        Assert.Equal(carCount, make.Cars.Count());
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    [InlineData(6, 1)]
    public void ShouldGetAllCarsForAMakeExplicitlyWithoutQueryFilters(int makeId, int carCount)
    {
        var make = Context.Makes.First(x => x.Id == makeId);
        IQueryable<Car> query = Context.Entry(make).Collection(m => m.Cars).Query().IgnoreQueryFilters();
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        query.Load();
        Assert.Equal(carCount, make.Cars.Count());
    }

    [Fact]
    public void ShouldGetAllHistoryRows()
    {
        var make = new Make { Name = "TestMake" };
        _makeRepo.Add(make);
        Thread.Sleep(2000);
        make.Name = "Updated Name";
        _makeRepo.Update(make);
        Thread.Sleep(2000);
        _makeRepo.Delete(make);
        var list = _makeRepo.GetAllHistory().Where(x => x.Entity.Id == make.Id).ToList();
        Assert.Equal(2, list.Count);
        Assert.Equal("TestMake", list[0].Entity.Name);
        Assert.Equal("Updated Name", list[1].Entity.Name);
        Assert.Equal(list[0].ValidTo, list[1].ValidFrom);
    }

    public override void Dispose()
    {
        _makeRepo.Dispose();
        base.Dispose();
    }
}
