namespace MasaFrameworkServiceCqrs.Service.Application.Example;

public class ExampleCommandHandler
{
    /// <summary>
    /// This use business DbContext
    /// </summary>
    private readonly MasaFrameworkServiceCqrsDbContext _dbContext;

    public ExampleCommandHandler(MasaFrameworkServiceCqrsDbContext dbContext) => _dbContext = dbContext;

    [EventHandler]
    public async Task CreateAsync(CreateExampleCommand command)
    {
        //TODO:Create logic
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateExampleCommand command)
    {
        //TODO:Update logic
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteExampleCommand command)
    {
        //TODO:Delete logic
    }
}
