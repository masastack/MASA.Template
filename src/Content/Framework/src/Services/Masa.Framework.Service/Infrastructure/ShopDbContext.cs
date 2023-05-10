namespace Masa.Framework.Service.Infrastructure;

public class ShopDbContext : MasaDbContext
{
    public DbSet<Order> Orders { get; set; } = default!;

    public ShopDbContext(MasaDbContextOptions<ShopDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        base.OnModelCreatingExecuting(builder);
#if (HasDdd)
        builder.Entity<Order>(b =>
        {
            b.OwnsOne(e => e.Address, build =>
            {
                build.Property(p => p.Address).HasColumnName(nameof(AddressValue.Address));
            });
        });
#endif
    }
}