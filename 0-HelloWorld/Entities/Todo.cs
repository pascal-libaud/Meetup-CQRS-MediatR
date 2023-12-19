namespace _0_HelloWorld.Entities;

public record Todo
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public bool IsDone { get; set; }
}