using _0_HelloWorld.Repositories;
using MediatR;

namespace _0_HelloWorld.Commands;

public record DeleteTodo(int Id) : IRequest;

public class DeleteTodoHandler : IRequestHandler<DeleteTodo>
{
    private readonly TodoRepository _todoRepository;

    public DeleteTodoHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task Handle(DeleteTodo request, CancellationToken cancellationToken)
    {
        _todoRepository.DeleteById(request.Id);
        return Task.CompletedTask;
    }
}