namespace Masa.Framework.Service.Services;

public class OrderService : ServiceBase
{
    public OrderService(IServiceCollection services) : base(services)
    {
        App.MapGet("/order/list", QueryList).Produces<List<Order>>()
#if (AddAuthorize)
            .WithName("GetOrders")
            .RequireAuthorization();
#else
            .WithName("GetOrders");
#endif
#if (UseCqrsDddMode || UseDddMode)
        App.MapPost("/order/placeOrder", PlaceOrder);
#endif
    }
#if (UseBasicMode)

    public async Task<IResult> QueryList(IEventBus eventBus)
    {
        var orderQueryEvent = new QueryOrderListEvent();
        await eventBus.PublishAsync(orderQueryEvent);
        return Results.Ok(orderQueryEvent.Orders);
    }
#elif (UseCqrsMode)

    public async Task<IResult> QueryList(IEventBus eventBus)
    {
        var query = new OrderQuery();
        await eventBus.PublishAsync(query);
        return Results.Ok(query.Result);
    }
#elif (UseDddMode)

    public async Task<IResult> QueryList(OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Results.Ok(orders);
    }

    public async Task<IResult> PlaceOrder(OrderDomainService orderDomainService)
    {
        await orderDomainService.PlaceOrderAsync();
        return Results.Ok();
    }
#endif
#if (UseCqrsDddMode)

    public async Task<IResult> QueryList(OrderDomainService orderDomainService)
    {
        var orders = await orderDomainService.QueryListAsync();
        return Results.Ok(orders);
    }

    public async Task<IResult> PlaceOrder(IEventBus eventBus)
    {
        var comman = new OrderCreateCommand();
        await eventBus.PublishAsync(comman);
        return Results.Ok();
    }
#endif
}