namespace Masa.Framework.Service.Application.Orders;

public class OrderQueryHandler
{
    readonly IOrderRepository _orderRepository;
    public OrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

#if (AddActor)
    [EventHandler]
    public async Task OrderListHandleAsync(OrderQuery query)
    {
        var actorId = new ActorId(Guid.NewGuid().ToString());
        var actor = ActorProxy.Create<IOrderActor>(actorId, nameof(OrderActor));
        query.Result = await actor.GetListAsync();
    }
#else
    [EventHandler]
    public async Task OrderListHandleAsync(OrderQuery query)
    {
        query.Result = await _orderRepository.GetListAsync();
    }
#endif
}

