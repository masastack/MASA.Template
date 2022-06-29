namespace Masa.Framework.Service.Infrastructure.Entities;

public class Order
{
    public int Id { get; set; }

    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;

    public string OrderNumber { get; set; } = default!;

    public string Address { get; set; } = default!;

    public Order(int id, string orderNumber)
    {
        Id = id;
        OrderNumber = orderNumber;
    }
}

