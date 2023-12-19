using _0_HelloWorld.Entities;
using _0_HelloWorld.Repositories;
using MediatR;

namespace _0_HelloWorld.Queries;

public record GetTodo(int Id) : IRequest<Todo>;

public class GetTodoHandler : IRequestHandler<GetTodo, Todo>
{
    private readonly TodoRepository _todoRepository;

    public GetTodoHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task<Todo> Handle(GetTodo request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_todoRepository.GetById(request.Id));
    }
}