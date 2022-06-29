namespace Masa.Framework.Service.Infrastructure.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetListAsync();
}

