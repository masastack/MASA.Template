namespace MasaFrameworkServiceCqrs.Service.DataAccess;

public class ExampleDbContext : MasaDbContext
{
    //public DbSet<UserEntity> { get; set; }

    public ExampleDbContext(MasaDbContextOptions<ExampleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        base.OnModelCreatingExecuting(modelBuilder);
        ConfigEntities(modelBuilder);
    }

    private static void ConfigEntities(ModelBuilder modelBuilder)
    {
        //TODO:Configure Entities.
    }
}
