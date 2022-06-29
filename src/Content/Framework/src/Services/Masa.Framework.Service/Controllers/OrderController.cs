namespace Masa.Framework.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
#if (UseBasicMode)
    [HttpGet("/list")]
    public IEnumerable<Order> List([FromServices] IEventBus eventBus)
    {
        var orderQueryEvent = new QueryOrderListEvent();
        eventBus.PublishAsync(orderQueryEvent);
        return orderQueryEvent.Orders;
    }
#elif (UseDddMode)
    [HttpGet("/list")]
    public async Task<IActionResult> List([FromServices] OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Ok(orders);
    }

    [HttpPost("/placeorder")]
    public async Task<IActionResult> PlaceOrder([FromServices] OrderDomainService orderDomainService)
    {
        await orderDomainService.PlaceOrderAsync();
        return Ok();
    }
#elif (UseCqrsMode)
    [HttpGet("/list")]
    public async Task<ActionResult<IEnumerable<Order>>> List([FromServices] IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Ok(query.Result);
    }
#endif

#if (UseCqrsDddMode)
    [HttpGet("/list")]
    public async Task<ActionResult<IEnumerable<Order>>> List([FromServices] IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Ok(query.Result);
    }

    [HttpPost("/placeorder")]
    public async Task<IResult> PlaceOrder([FromServices] IEventBus eventBus)
    {
        var comman = new OrderCreateCommand();
        await eventBus.PublishAsync(comman);
        return Results.Ok();
    }
#endif
}

