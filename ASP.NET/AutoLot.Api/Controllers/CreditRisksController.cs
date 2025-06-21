namespace AutoLot.Api.Controllers;

public class CreditRisksController : BaseCrudController<CreditRisk, CreditRisksController>
{
    public CreditRisksController(IAppLogging<CreditRisksController> logger, ICreditRiskRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
