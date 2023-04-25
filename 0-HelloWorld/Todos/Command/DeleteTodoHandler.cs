using MediatR;

namespace _0_HelloWorld.Todos.Command;

public class DeleteTodoHandler : IRequestHandler<DeleteTodo>
{
    public Task Handle(DeleteTodo request, CancellationToken cancellationToken)
    {
        TodoRepository.DeleteById(request.Id);
        return Task.CompletedTask;
    }
}