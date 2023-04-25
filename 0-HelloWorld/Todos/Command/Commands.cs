using MediatR;

namespace _0_HelloWorld.Todos.Command;

public record SetIsDone(int Id) : IRequest;

public record DeleteTodo(int Id) : IRequest;