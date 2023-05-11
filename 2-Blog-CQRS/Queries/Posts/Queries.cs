using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Queries.Posts;

public record GetAllPosts : IQuery<PostDTO[]>;

public record GetDetailedPost(int Id) : IQuery<PostDetailDTO>;