namespace _0_HelloWorld.Todos;

public record Todo
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public bool IsDone { get; set; }
}

public static class TodoRepository
{
    private static readonly List<Todo> _todos;

    static TodoRepository()
    {
        _todos = new List<Todo>
        {
            new Todo
            {
                Id = 1,
                Title = "Terminer la présentation du meetup",
                IsDone = true,
            },
            new Todo
            {
                Id = 2,
                Title = "Demander une augmentation à mon manager",
                IsDone = false,
            },
            new Todo
            {
                Id = 3,
                Title = "Penser à remercier les participants du meetup",
                IsDone = false,
            }
        };
    }

    public static Todo[] GetTodos()
    {
        return _todos.ToArray();
    }

    public static Todo GetById(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if(todo != null)
            return todo;

        throw new ArgumentException();
    }

    public static void DeleteById(int id)
    {
        _todos.RemoveAll(t => t.Id == id);
    }
}