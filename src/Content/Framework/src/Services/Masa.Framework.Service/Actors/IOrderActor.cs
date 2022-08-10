namespace Masa.Framework.Service.Actors;

public interface IOrderActor : IActor
{
    Task<List<Order>> GetListAsync();
}