using MediatR;

namespace _0_HelloWorld.Todos.Query;

public class GetAllTodosHandler : IRequestHandler<GetAllTodos, Todo[]>
{
    public Task<Todo[]> Handle(GetAllTodos request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TodoRepository.GetTodos());
    }
}