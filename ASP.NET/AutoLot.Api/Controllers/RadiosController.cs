namespace AutoLot.Api.Controllers;

public class RadiosController : BaseCrudController<Radio, RadiosController>
{
    public RadiosController(IAppLogging<RadiosController> logger, IRadioRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
