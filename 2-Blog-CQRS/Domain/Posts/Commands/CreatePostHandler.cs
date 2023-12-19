using _2_Blog_CQRS.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Posts.Commands;

public class CreatePostHandler : IRequestHandler<CreatePost>
{
    private readonly BlogContext _context;

    public CreatePostHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var author = await _context.Users.FirstOrDefaultAsync(x => x.Name == request.Author, cancellationToken);
        if (author == null)
            throw new NotFoundException();

        var post = new Post
        {
            Author = author,
            Title = request.Title,
            Content = request.Content,
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class CreatePostValidator : AbstractValidator<CreatePost>
{
    public CreatePostValidator()
    {
        RuleFor(p => p.Title).MinimumLength(5).MaximumLength(255);
        RuleFor(p => p.Content).MinimumLength(30);
    }
}