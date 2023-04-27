namespace MasaFrameworkService.Services;

public class ExampleService : ServiceBase
{
    public Task<string> GetAsync()
    {
        return Task.FromResult("Hello,MASA Framework!");
    }
}
