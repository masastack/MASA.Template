namespace Masa.Framework.Service.Domain.Aggregates.Orders;

public class Order : AggregateRoot<int>
{
    public Order(int id, string orderNumber) : this(id, orderNumber, new())
    {
    }

    public Order(int id, string orderNumber, AddressValue address)
    {
        Id = id;
        OrderNumber = orderNumber;
        Address = address;
        Items = new List<OrderItem>();
    }

    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;

    public string OrderNumber { get; private set; } = default!;

    public AddressValue Address { get; private set; } = new();

    public List<OrderItem> Items { get; private set; }
}

