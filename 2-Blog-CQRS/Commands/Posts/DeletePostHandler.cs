using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Commands.Posts;

public class DeletePostHandler : IRequestHandler<DeletePost>
{
    private readonly BlogContext _context;

    public DeletePostHandler(BlogContext context)
    {
        _context = context;
    }

    public Task Handle(DeletePost request, CancellationToken cancellationToken)
    {
        return _context.Posts.Where(p => p.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
    }
}

public sealed class DeletePostValidator : AbstractValidator<DeletePost>
{
    public DeletePostValidator(BlogContext context)
    {
        RuleFor(d => d.Id).MustAsync((id, cancellationToken) => context.Posts.AnyAsync(p => p.Id == id, cancellationToken));
    }
}
