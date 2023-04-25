using MediatR;

namespace _0_HelloWorld.Todos.Query;

public record GetAllTodos : IRequest<Todo[]>;

public record GetTodo(int Id) : IRequest<Todo>;
