using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Commands;

public record CreatePost(string Title, string Content, string Author) : ICommand;

public record DeletePost(int Id) : ICommand;