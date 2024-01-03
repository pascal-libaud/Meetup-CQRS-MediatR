using _1_Blog_CQRS_Less.Common;
using _1_Blog_CQRS_Less.Helpers;
using Microsoft.EntityFrameworkCore;

namespace _1_Blog_CQRS_Less.Services;

public class PostService
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
        _logger.LogInformation("Get posts");
        return await _context.Posts
                             .Select(p => new PostDTO { Id = p.Id, Title = p.Title, Author = p.Author.Name })
                             .ToArrayAsync(cancellationToken);
    }

    public async Task<PostDTO> GetPost(int id, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (post == null)
            throw new NotFoundException();

        return new PostDTO
            {
                Id = id,
                Title = post.Title,
                Author = post.Author.Name,
                Content = post.Content,
                Comments = post.Comments.Select(c => new CommentDTO(c.Content, c.Author.Name)).ToArray()
            };
    }

    public async Task CreatePost(PostDTO postDTO, CancellationToken cancellationToken)
    {
        var author = await _context.Users.FirstOrDefaultAsync(x => x.Name == postDTO.Author, cancellationToken);
        if (author == null)
            throw new NotFoundException();

        var post = new Post
        {
            Author = author,
            Title = postDTO.Title,
            Content = postDTO.Content,
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePost(int id, CancellationToken cancellationToken)
    {
        await _context.Posts.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}