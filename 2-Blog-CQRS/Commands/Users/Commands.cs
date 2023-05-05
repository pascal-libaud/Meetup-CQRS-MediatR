using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Commands.Users;

public record CreateUser(string Name) : ICommand<int>; // TODO Handler

public record RenameUser(int Id, string Name) : ICommand; // TODO Handler

public record DeleteUser(int Id): ICommand; // TODO Handler