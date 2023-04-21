namespace MasaFrameworkServiceCqrs.Service.DataAccess;

public class MasaFrameworkServiceCqrsDbContext : MasaDbContext
{
    //public DbSet<UserEntity> { get; set; }

    public MasaFrameworkServiceCqrsDbContext(MasaDbContextOptions<MasaFrameworkServiceCqrsDbContext> options) : base(options)
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
