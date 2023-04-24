namespace Masa.Framework.Service.Infrastructure.Middleware;

public class ValidatorMiddleware<TEvent> : EventMiddleware<TEvent>
    where TEvent : notnull, IEvent
{
    private readonly ILogger<ValidatorMiddleware<TEvent>> _logger;

#if (UseFluentValidation)
    private readonly IEnumerable<IValidator<TEvent>> _validators;
#endif

    public ValidatorMiddleware(
#if (UseFluentValidation)
        IEnumerable<IValidator<TEvent>> validators, 
#endif
        ILogger<ValidatorMiddleware<TEvent>> logger)
    {
#if (UseFluentValidation)
        _validators = validators;
#endif
        _logger = logger;
    }

    public override async Task HandleAsync(TEvent action, EventHandlerDelegate next)
    {
        var typeName = action.GetType().FullName;

        _logger.LogInformation("----- Validating command {CommandType}", typeName);

#if (UseFluentValidation)
        var failures = _validators
            .Select(v => v.Validate(action))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, action, failures);

            throw new ValidationException("Validation exception", failures);
        }
#endif

        await next();
    }
}