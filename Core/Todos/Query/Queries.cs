using Core.Common;

namespace Core.Todos.Query;

public record GetAllTodos : IQuery<Todo[]>;

public record GetTodo(Guid Id) : IQuery<Todo>;
