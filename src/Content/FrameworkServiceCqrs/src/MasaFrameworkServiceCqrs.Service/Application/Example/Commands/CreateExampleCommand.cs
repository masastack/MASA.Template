namespace MasaFrameworkServiceCqrs.Service.Application.Example.Commands;

public record CreateExampleCommand(ExampleCreateUpdateDto Dto) : Command
{
}
