namespace _1_Blog_CQRS_Less.Models;

public class Post
{
    public int Id { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}