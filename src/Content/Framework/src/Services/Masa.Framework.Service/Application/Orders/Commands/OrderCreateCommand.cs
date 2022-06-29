namespace Masa.Framework.Service.Application.Orders.Commands;

#if (UseCqrsDddMode)
public record OrderCreateCommand : DomainCommand
#else
public record OrderCreateCommand : Command
#endif
{
    public List<OrderItem> Items { get; set; } = new ();
}

