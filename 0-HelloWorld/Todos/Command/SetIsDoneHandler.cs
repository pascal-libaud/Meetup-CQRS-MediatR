using MediatR;

namespace _0_HelloWorld.Todos.Command;

public class SetIsDoneHandler : IRequestHandler<SetIsDone>
{
    public Task Handle(SetIsDone request, CancellationToken cancellationToken)
    {
        var todo = TodoRepository.GetById(request.Id);
        todo.IsDone = true;
        return Task.CompletedTask;
    }
}