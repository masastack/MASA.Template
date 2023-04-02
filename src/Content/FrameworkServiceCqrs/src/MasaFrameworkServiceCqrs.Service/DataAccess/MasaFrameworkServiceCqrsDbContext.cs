namespace MasaFrameworkServiceCqrs.Service.DataAccess;

public class MasaFrameworkServiceCqrsDbContext : MasaDbContext
{
    //TODO:Add DbSet<UserEntity> to this.

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
