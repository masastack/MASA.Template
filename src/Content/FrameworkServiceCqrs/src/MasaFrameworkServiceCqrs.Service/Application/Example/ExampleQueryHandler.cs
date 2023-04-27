namespace MasaFrameworkServiceCqrs.Service.Application.Example;

public class ExampleQueryHandler
{
    /// <summary>
    /// This can use query's DbContext
    /// </summary>
    private readonly ExampleDbContext _dbContext;

    public ExampleQueryHandler(ExampleDbContext dbContext) => _dbContext = dbContext;

    [EventHandler]
    public Task GetListAsync(ExampleGetListQuery command)
    {
        //TODO:Get logic
        return Task.CompletedTask;
    }
}
