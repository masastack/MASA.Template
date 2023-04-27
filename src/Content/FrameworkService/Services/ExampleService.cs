namespace MasaFrameworkService.Services;

public class ExampleService : ServiceBase
{
    public async Task<string> GetAsync()
    {
        return "Hello,MASA Framework!";
    }
}
