namespace AutoLot.Api.Controllers;

public class OrdersController : BaseCrudController<Order, OrdersController>
{
    public OrdersController(IAppLogging<OrdersController> logger, IOrderRepo mainRepo) : base(logger, mainRepo)
    {
    }
}
