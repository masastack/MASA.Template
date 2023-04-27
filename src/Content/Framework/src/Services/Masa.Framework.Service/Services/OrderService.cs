namespace Masa.Framework.Service.Services;

public class OrderService : ServiceBase
{
    public OrderService()
    {
    }
#if (UseBasicMode)

#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> QueryList(IEventBus eventBus)
    {
        var orderQueryEvent = new QueryOrderListEvent();
        await eventBus.PublishAsync(orderQueryEvent);
        return Results.Ok(orderQueryEvent.Orders);
    }
#elif (UseCqrsMode)

#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> QueryList(IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Results.Ok(query.Result);
    }
#elif (UseDddMode)

#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> QueryList(OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Results.Ok(orders);
    }
    
#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> PlaceOrder(OrderDomainService orderDomainService)
    {
        await orderDomainService.PlaceOrderAsync();
        return Results.Ok();
    }
#endif
#if (UseCqrsDddMode)

#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> QueryList(OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Results.Ok(orders);
    }
    
#if (AddAuthorize)
    [Authorize]
#endif
    public async Task<IResult> PlaceOrder(IEventBus eventBus)
    {
        var comman = new OrderCreateCommand();
        await eventBus.PublishAsync(comman);
        return Results.Ok();
    }
#endif
}