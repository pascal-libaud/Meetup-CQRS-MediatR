using Core.Common;

namespace Core.Todos.Command;

public record SetDone(Guid Id) : ICommand;

public record DeleteTodo(Guid Id) : ICommand;