namespace _2_Blog_CQRS.Domain;

public class Post
{
    public int Id { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Post> Posts { get; set; }
}

public class Comment
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User Author { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }
}