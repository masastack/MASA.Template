namespace Masa.Framework.Service.Infrastructure.Handlers;

#if (!HasDdd)
public class OrderEventHandler
{
    readonly IOrderRepository _orderRepository;

    public OrderEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

#if (AddActor)
    [EventHandler(Order = 1)]
    public async Task HandleAsync (QueryOrderListEvent @event)
    {

        var actorId = new ActorId(Guid.NewGuid().ToString());
        var actor = ActorProxy.Create<IOrderActor>(actorId, nameof(OrderActor));
        @event.Orders = await actor.GetListAsync();
    }
#else
    [EventHandler(Order = 1)]
    public async Task HandleAsync(QueryOrderListEvent @event)
    {
        @event.Orders = await _orderRepository.GetListAsync();
    }
#endif
}

public class OrderEventAfterHandler : IEventHandler<QueryOrderListEvent>
{
    public Task HandleAsync(QueryOrderListEvent @event, CancellationToken cancellationToken = default)
    {
        //todo query after
        return Task.CompletedTask;
    }
}
#endif