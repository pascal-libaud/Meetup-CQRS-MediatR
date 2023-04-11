using System.Collections.Immutable;
using Core.Common;

namespace Core.Todos.Repos;

public class Repository
{
    private static ImmutableArray<Todo> _todos = ImmutableArray<Todo>.Empty;

    public ValueTask<Todo[]> GetTodos()
    {
        return ValueTask.FromResult(_todos.ToArray());
    }

    public ValueTask<Result<Todo>> GetById(Guid id)
    {
        var todo = _todos.FirstOrDefault(x => x.Id == id);

        if (todo is null)
            return ValueTask.FromResult(Result.Fail<Todo>($"Pas de todo {id}"));
        else
            return ValueTask.FromResult(Result.Success(todo));
    }

    public ValueTask Add(Todo todo)
    {
        var existing = _todos.FirstOrDefault(x => x.Id == todo.Id);
        if (existing != default) throw new Exception("todo");
        _todos = _todos.Add(todo);
        return ValueTask.CompletedTask;
    }

    public ValueTask<Result> DeleteById(Guid id)
    {
        var todo = _todos.FirstOrDefault(x => x.Id == id);

        if (todo != default)
        {
            _todos = _todos.Replace(todo, todo with { IsDeleted = true });
            return ValueTask.FromResult(Result.Successful);
        }
        else
            return ValueTask.FromResult(Result.Fail($"Pas de todo {id}"));
    }

    public ValueTask<Result> UpdateById(Guid id, Todo newTodo)
    {
        var todo = _todos.FirstOrDefault(x => x.Id == id);

        if (todo != default)
        {
            _todos = _todos.Replace(todo, newTodo);
            return ValueTask.FromResult(Result.Successful);
        }
        else
            return ValueTask.FromResult(Result.Fail($"Pas de todo {id}"));
    }
}