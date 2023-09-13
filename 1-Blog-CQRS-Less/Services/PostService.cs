using _1_Blog_CQRS_Less.Common;
using _1_Blog_CQRS_Less.Helpers;
using _1_Blog_CQRS_Less.Models;
using Microsoft.EntityFrameworkCore;

namespace _1_Blog_CQRS_Less.Services;

public class PostService : IPostService
{
    private readonly BlogContext _context;
    private readonly PerfHelper _perfHelper;
    private readonly ILogger<PostService> _logger;

    public PostService(BlogContext context, PerfHelper perfHelper, ILogger<PostService> logger)
    {
        _context = context;
        _perfHelper = perfHelper;
        _logger = logger;
    }

    public async Task<PostDTO[]> GetPosts(CancellationToken cancellationToken)
    {
        return await _context.Posts
                             .Select(p => new PostDTO(p.Id, p.Title, p.Author.Name))
                             .ToArrayAsync(cancellationToken);
    }

    public async Task<PostDetailDTO> GetPost(int id, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (post == null)
            throw new NotFoundException();

        return new PostDetailDTO(
            post.Title,
            post.Author.Name,
            post.Content,
            post.Comments.Select(c => new CommentDTO(c.Content, c.Author.Name)).ToArray());
        ;
    }

    public async Task CreatePost(CreatePost createPost, CancellationToken cancellationToken)
    {
        var author = await _context.Users.FirstOrDefaultAsync(x => x.Name == createPost.Author, cancellationToken);
        if (author == null)
            throw new NotFoundException();

        var post = new Post
        {
            Author = author,
            Title = createPost.Title,
            Content = createPost.Content,
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePost(int id, CancellationToken cancellationToken)
    {
        await _context.Posts.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}