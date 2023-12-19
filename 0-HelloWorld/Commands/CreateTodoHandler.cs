using _0_HelloWorld.Repositories;
using MediatR;

namespace _0_HelloWorld.Commands;

public record CreateTodo(string Title) : IRequest;

public class CreateTodoHandler : IRequestHandler<CreateTodo>
{
    private readonly TodoRepository _todoRepository;

    public CreateTodoHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task Handle(CreateTodo request, CancellationToken cancellationToken)
    {
        _todoRepository.Create(request.Title);
        return Task.CompletedTask;
    }
}