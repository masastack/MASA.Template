namespace Masa.Framework.Service.Infrastructure.Events;

#if (!HasDdd)
public record QueryOrderListEvent : Event
{
    public List<Order> Orders { get; set; } = new();
}
#endif