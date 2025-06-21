namespace AutoLot.Api.Controllers;

public class CustomersController : BaseCrudController<Customer, CustomersController>
{
    public CustomersController(IAppLogging<CustomersController> logger, ICustomerRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
