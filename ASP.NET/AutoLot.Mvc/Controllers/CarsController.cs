namespace AutoLot.Mvc.Controllers;

public class CarsController : BaseCrudController<Car, CarsController>
{
    private readonly IMakeDataService _lookupDataService;

    public CarsController(IAppLogging<CarsController> appLogging, ICarDataService mainDataService,
        IMakeDataService lookupDataService) : base(appLogging, mainDataService)
    {
        _lookupDataService = lookupDataService;
    }

    protected override async Task<SelectList> GetLookupValuesAsync()
        => new SelectList(await _lookupDataService.GetAllAsync(), nameof(Make.Id), nameof(Make.Name));

    [HttpGet("{makeId}/{makeName}")]
    public async Task<IActionResult> ByMakeAsync(int makeId, string makeName)
    {
        ViewBag.MakeName = makeName;
        return View(await ((ICarDataService)MainDataService).GetAllByMakeIdAsync(makeId));
    }
}
