namespace Masa.Framework.Bff.Services;

public class OrderService : ServiceBase
{
    readonly OrderCaller _orderCaller;

    public OrderService(IServiceCollection services, OrderCaller orderCaller) : base(services)
    {
        _orderCaller = orderCaller;
        App.MapGet("/api/v1/orders", GetListAsync);
    }

    public async Task<IResult> GetListAsync()
    {
        var data = await _orderCaller.GetListAsync();
        return Results.Ok(data);
    }
}

