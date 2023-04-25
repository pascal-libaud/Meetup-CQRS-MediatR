using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Commands;

public class CreatePostHandler : IRequestHandler<CreatePost>
{
    private readonly BlogContext _context;

    public CreatePostHandler(BlogContext context)
    {
        _context = context;
    }

    public Task Handle(CreatePost request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public sealed class CreatePostValidator : AbstractValidator<CreatePost>
{
    public CreatePostValidator(BlogContext context)
    {
        RuleFor(p => p.Title).MinimumLength(5).MaximumLength(255);
        RuleFor(p => p.Content).MinimumLength(30);
        RuleFor(p => p.Author).MustAsync((author, cancellationToken) => context.Users.AnyAsync(u => u.Name == author, cancellationToken));
    }
}