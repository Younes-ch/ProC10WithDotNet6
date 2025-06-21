namespace AutoLot.Api.Controllers;

public class MakesController : BaseCrudController<Make, MakesController>
{
    public MakesController(IAppLogging<MakesController> logger, IMakeRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
