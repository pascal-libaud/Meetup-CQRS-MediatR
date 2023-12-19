using _2_Blog_CQRS.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Posts.Commands;

public class UpdatePostHandler : IRequestHandler<UpdatePost>
{
    private readonly BlogContext _context;

    public UpdatePostHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (post == null)
            throw new NotFoundException();

        post.Title = request.Title;
        post.Content = post.Content;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class UpdatePostValidator : AbstractValidator<UpdatePost>
{
    public UpdatePostValidator()
    {
        RuleFor(p => p.Id).GreaterThan(0);
        RuleFor(p => p.Title).MinimumLength(5).MaximumLength(255);
        RuleFor(p => p.Content).MinimumLength(30);
    }
}