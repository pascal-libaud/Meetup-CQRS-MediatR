using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Commands.Posts;

public record CreatePost(string Title, string Content, string Author) : ICommand;

public record UpdatePost(int Id, string Title, string Content) : ICommand;

public record DeletePost(int Id) : ICommand;