namespace AutoLot.Api.Controllers;

public class CarDriversController : BaseCrudController<CarDriver, CarDriversController>
{
    public CarDriversController(IAppLogging<CarDriversController> logger, ICarDriverRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
