using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Posts.Queries;

public class GetAllPostsHandler : IRequestHandler<GetAllPosts, PostDTO[]>
{
    private readonly BlogContext _context;
    private readonly ILogger<GetAllPostsHandler> _logger;

    public GetAllPostsHandler(BlogContext context, ILogger<GetAllPostsHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<PostDTO[]> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get posts");
        return _context.Posts
            .Select(p => new PostDTO(p.Id, p.Title, p.Author.Name))
            .ToArrayAsync(cancellationToken);
    }
}