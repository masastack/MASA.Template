namespace Masa.Framework.Service.Application.Orders.Queries;

#if (UseCqrsDddMode)
public record OrderQuery : DomainQuery<List<Order>>
#else
public record OrderQuery : Query<List<Order>>
#endif
{
    public override List<Order> Result { get; set; } = new();
}


