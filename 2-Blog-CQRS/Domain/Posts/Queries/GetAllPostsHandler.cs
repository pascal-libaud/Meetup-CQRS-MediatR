using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Posts.Queries;

public class GetAllPostsHandler : IRequestHandler<GetAllPosts, PostDTO[]>
{
    private readonly BlogContext _context;

    public GetAllPostsHandler(BlogContext context)
    {
        _context = context;
    }

    public Task<PostDTO[]> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return _context.Posts
            .Select(p => new PostDTO(p.Id, p.Title, p.Author.Name))
            .ToArrayAsync(cancellationToken);
    }
}