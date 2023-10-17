namespace _0_HelloWorld.Todos;

public static class TodoRepository
{
    private static readonly List<Todo> _todos;

    static TodoRepository()
    {
        _todos = new List<Todo>
        {
            new() { Id = 1, Title = "Terminer la présentation du meetup", IsDone = true },
            new() { Id = 2, Title = "Demander une augmentation à mon manager", IsDone = false },
            new() { Id = 3, Title = "Penser à remercier les participants du meetup", IsDone = false }
        };
    }

    public static Todo[] GetTodos()
    {
        return _todos.ToArray();
    }

    public static void Create(string title)
    {
        var id = _todos.Max(t => t.Id) + 1;
        _todos.Add(new Todo { Id = id, IsDone = false, Title = title });
    }

    public static Todo GetById(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
            return todo;

        throw new ArgumentException();
    }

    public static void DeleteById(int id)
    {
        _todos.RemoveAll(t => t.Id == id);
    }
}