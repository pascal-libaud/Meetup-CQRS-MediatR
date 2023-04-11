using Core.Todos.Repos;
using MediatR;

namespace Core.Todos.Query;

public class GetAllTodosHandler : IRequestHandler<GetAllTodos, Todo[]>
{
    private readonly Repository _repository;

    public GetAllTodosHandler(Repository repository)
    {
        _repository = repository;
    }

    public Task<Todo[]> Handle(GetAllTodos request, CancellationToken cancellationToken)
    {
        return _repository.GetTodos().AsTask();
    }
}