namespace AutoLot.Api.Controllers;

public class DriversController : BaseCrudController<Driver, DriversController>
{
    public DriversController(IAppLogging<DriversController> logger, IDriverRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
