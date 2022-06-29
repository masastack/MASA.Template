namespace Masa.Framework.Service.Infrastructure.Repositories;

#if (HasDdd)
public class OrderRepository : Repository<ShopDbContext, Order>, IOrderRepository
{
    public OrderRepository(ShopDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }
#else
public class OrderRepository : IOrderRepository
{
#endif
    public async Task<List<Order>> GetListAsync()
    {
        var data = Enumerable.Range(1, 5).Select(index =>
                  new Order(index, DateTimeOffset.Now.ToUnixTimeSeconds().ToString())).ToList();
        return await Task.FromResult(data);
    }
}