namespace Masa.Framework.Service.Application.Orders;

public class OrderCommandHandler
{
#if (UseCqrsDddMode)
    private readonly OrderDomainService _domainService;

    public OrderCommandHandler(OrderDomainService domainService)
    {
        _domainService = domainService;
    }
#else
    public OrderCommandHandler()
    {

    }
#endif

    [EventHandler(Order = 1)]
    public async Task CreateHandleAsync(OrderCreateCommand command)
    {
#if (UseCqrsDddMode)
        await _domainService.PlaceOrderAsync();
#endif
        //todo your work
        await Task.CompletedTask;
    }
}

public class OrderStockHandler : CommandHandler<OrderCreateCommand>
{
    public override Task CancelAsync(OrderCreateCommand comman, CancellationToken cancellationToken = default)
    {
        //todo cancel todo callback 
        return Task.CompletedTask;
    }

    [EventHandler(FailureLevels = FailureLevels.ThrowAndCancel)]
    public override Task HandleAsync(OrderCreateCommand comman, CancellationToken cancellationToken = default)
    {
        //todo decrease stock
        return Task.CompletedTask;
    }

    [EventHandler(0, FailureLevels.Ignore, IsCancel = true)]
    public Task AddCancelLogs(OrderCreateCommand query)
    {
        //todo increase stock
        return Task.CompletedTask;
    }
}