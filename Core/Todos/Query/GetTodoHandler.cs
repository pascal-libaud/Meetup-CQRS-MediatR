using Core.Todos.Repos;
using MediatR;

namespace Core.Todos.Query;

public class GetTodoHandler : IRequestHandler<GetTodo, Todo>
{
    private readonly Repository _repository;

    public GetTodoHandler(Repository repository)
    {
        _repository = repository;
    }

    public async Task<Todo> Handle(GetTodo request, CancellationToken cancellationToken)
    {
        var t = await _repository.GetById(request.Id);
        if (t.TryGetValue(out var todo))
            return todo;
        else
            throw new ArgumentException(t.ErrorMessage);
    }
}