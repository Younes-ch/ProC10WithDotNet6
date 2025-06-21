namespace AutoLot.Dal.Tests.IntegrationTests;

[Collection("Integration Tests")]
public class OrderTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
{
    private readonly IOrderRepo _orderRepo;

    public OrderTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _orderRepo = new OrderRepo(Context);
    }

    [Fact]
    public void ShouldGetAllOrdersExceptFiltered()
    {
        var query = Context.Orders.AsQueryable();
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        var orders = query.ToList();
        Assert.NotEmpty(orders);
        Assert.Equal(4, orders.Count);
    }

    [Fact]
    public void ShouldGetAllOrders()
    {
        var query = Context.Orders.IgnoreQueryFilters();
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        List<Order> orders = query.ToList();
        Assert.NotEmpty(orders);
        Assert.Equal(5, orders.Count);
    }

    public override void Dispose()
    {
        _orderRepo.Dispose();
        base.Dispose();
    }
}
