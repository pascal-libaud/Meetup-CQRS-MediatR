using _0_HelloWorld.Entities;
using _0_HelloWorld.Repositories;
using MediatR;

namespace _0_HelloWorld.Queries;

public record GetAllTodos : IRequest<Todo[]>;

public class GetAllTodosHandler : IRequestHandler<GetAllTodos, Todo[]>
{
    private readonly TodoRepository _todoRepository;

    public GetAllTodosHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task<Todo[]> Handle(GetAllTodos request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_todoRepository.GetTodos());
    }
}