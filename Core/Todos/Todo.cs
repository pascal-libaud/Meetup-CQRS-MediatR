namespace Core.Todos;

public record Todo
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public bool IsDone { get; init; }

    public bool IsDeleted { get; init; }

    public static Todo Empty { get; } = new Todo { Id = Guid.Empty, Title = string.Empty };
}