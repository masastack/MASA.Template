namespace Masa.Framework.Contracts.Order.Model;

public class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public AddressValue Address { get; set; }

    public DateTime CreationTime { get; set; }
}

public class AddressValue
{
    public string Address { get; set; }

    public override string ToString() => Address;
}