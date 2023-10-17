using MediatR;

namespace _0_HelloWorld.Todos.Command;

public class CreateTodoHandler : IRequestHandler<CreateTodo>
{
    public Task Handle(CreateTodo request, CancellationToken cancellationToken)
    {
        TodoRepository.Create(request.Title);
        return Task.CompletedTask;
    }
}