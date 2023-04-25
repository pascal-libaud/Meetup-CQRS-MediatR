using MediatR;

namespace _0_HelloWorld.Todos.Query;

public class GetTodoHandler : IRequestHandler<GetTodo, Todo>
{
    public Task<Todo> Handle(GetTodo request, CancellationToken cancellationToken)
    {
        return Task.FromResult(TodoRepository.GetById(request.Id));
    }
}