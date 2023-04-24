namespace MasaFrameworkServiceCqrs.Service.Application.Example;

public class ExampleQueryHandler
{
    /// <summary>
    /// This can use query's DbContext
    /// </summary>
    private readonly MasaFrameworkServiceCqrsDbContext _dbContext;

    public ExampleQueryHandler(MasaFrameworkServiceCqrsDbContext dbContext) => _dbContext = dbContext;

    [EventHandler]
    public async Task GetListAsync(ExampleGetListQuery command)
    {
        //TODO:Get logic
    }
}
