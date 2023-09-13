using _1_Blog_CQRS_Less.Models;

namespace _1_Blog_CQRS_Less.Services
{
    public interface IPostService
    {
        Task CreatePost(CreatePost createPost, CancellationToken cancellationToken);
        Task DeletePost(int id, CancellationToken cancellationToken);
        Task<PostDetailDTO> GetPost(int id, CancellationToken cancellationToken);
        Task<PostDTO[]> GetPosts(CancellationToken cancellationToken);
    }
}