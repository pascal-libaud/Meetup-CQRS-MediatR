using Faker;

namespace Core.Todos.Repos;

public static class TodoSeeder
{
    private static readonly Todo[] _todos;

    static TodoSeeder()
    {
        _todos = Enumerable.Range(0, 12).Select(_ =>
            new Todo
            {
                Id = Guid.NewGuid(),
                Title = Name.FullName(NameFormats.StandardWithMiddle),
                IsDeleted = Faker.Boolean.Random(),
                IsDone = Faker.Boolean.Random()
            }).ToArray();
    }

    public static async ValueTask Seed(Repository repository)
    {
        foreach (var todo in _todos)
        {
            await repository.Add(todo);
        }
    }
}