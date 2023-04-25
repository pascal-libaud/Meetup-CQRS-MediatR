namespace _1_Blog_CQRS_Less.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}