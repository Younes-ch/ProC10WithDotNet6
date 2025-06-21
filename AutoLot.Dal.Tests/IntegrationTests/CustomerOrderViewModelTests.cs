namespace AutoLot.Dal.Tests.IntegrationTests;

[Collection("Integration Tests")]
public class CustomerOrderViewModelTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
{
    private readonly ICustomerOrderViewModelRepo _customerOrderViewModelRepo;

    public CustomerOrderViewModelTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _customerOrderViewModelRepo = new CustomerOrderViewModelRepo(Context);
    }

    [Fact]
    public void ShouldGetAllViewModels()
    {
        var qs = Context.CustomerOrderViewModels.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        List<Models.ViewModels.CustomerOrderViewModel> list = Context.CustomerOrderViewModels.ToList();
        Assert.NotEmpty(list);
        Assert.Equal(5, list.Count);
    }

    public override void Dispose()
    {
        _customerOrderViewModelRepo.Dispose();
        base.Dispose();
    }
}
