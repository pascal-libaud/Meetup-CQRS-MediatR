namespace _1_Blog_CQRS_Less.Models;

public class Comment
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User Author { get; set; }
    public string Content { get; set; }
}