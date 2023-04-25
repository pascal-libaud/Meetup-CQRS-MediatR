using _2_Blog_CQRS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Queries;

public class GetDetailedPostHandler : IRequestHandler<GetDetailedPost, PostDetailDTO>
{
    private readonly BlogContext _context;

    public GetDetailedPostHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<PostDetailDTO> Handle(GetDetailedPost request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (post == null)
            throw new NotFoundException();

        return new PostDetailDTO(post.Title, post.Author.Name, post.Content, post.Comments.Select(c => new CommentDTO(c.Content, c.Author.Name)).ToArray());
    }
}