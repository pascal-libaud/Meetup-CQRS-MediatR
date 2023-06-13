using _2_Blog_CQRS.Helpers;
using FluentValidation;
using MediatR;

namespace _2_Blog_CQRS.Pipelines;

public class FluentValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public FluentValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = await _validators.SelectAsync(async v => await v.ValidateAsync(request, cancellationToken))
            .SelectManyAsync(result => result.Errors)
            .WhereAsync(f => f != null)
            .ToListAsync();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}