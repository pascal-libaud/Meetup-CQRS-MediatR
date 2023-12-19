using _0_HelloWorld.Repositories;
using MediatR;

namespace _0_HelloWorld.Commands;

public record SetIsDone(int Id) : IRequest;

public class SetIsDoneHandler : IRequestHandler<SetIsDone>
{
    private readonly TodoRepository _todoRepository;

    public SetIsDoneHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task Handle(SetIsDone request, CancellationToken cancellationToken)
    {
        var todo = _todoRepository.GetById(request.Id);
        todo.IsDone = true;
        return Task.CompletedTask;
    }
}