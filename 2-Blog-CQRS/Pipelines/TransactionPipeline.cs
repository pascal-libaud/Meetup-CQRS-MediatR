using _2_Blog_CQRS.Common;
using _2_Blog_CQRS.Domain;
using MediatR;

namespace _2_Blog_CQRS.Pipelines;

public class TransactionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly BlogContext _context;

    public TransactionPipeline(BlogContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICommand)
        {
            return await next();
        }

        if (_context.Database.CurrentTransaction is not null)
        {
            return await next();
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}