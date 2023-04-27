namespace Masa.Framework.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]s/[action]")]
public class OrderController : ControllerBase
{
#if (UseBasicMode)
    [HttpGet]
    public IEnumerable<Order> QueryList([FromServices] IEventBus eventBus)
    {
        var orderQueryEvent = new QueryOrderListEvent();
        eventBus.PublishAsync(orderQueryEvent);
        return orderQueryEvent.Orders;
    }
#elif (UseDddMode)
    [HttpGet]
    public async Task<IActionResult> QueryList([FromServices] OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Ok(orders);
    }
    
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromServices] OrderDomainService orderDomainService)
    {
        await orderDomainService.PlaceOrderAsync();
        return Ok();
    }
#elif (UseCqrsMode)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> QueryList([FromServices] IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Ok(query.Result);
    }
#endif

#if (UseCqrsDddMode)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> QueryList([FromServices] IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Ok(query.Result);
    }

    [HttpPost]
    public async Task<IResult> PlaceOrder([FromServices] IEventBus eventBus)
    {
        var comman = new OrderCreateCommand();
        await eventBus.PublishAsync(comman);
        return Results.Ok();
    }
#endif
}