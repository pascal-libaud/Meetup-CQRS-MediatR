namespace _1_Blog_CQRS_Less;

public class Post
{
    public int Id { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Comments> Comments { get; set; } = new List<Comments>();
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Post> Posts { get; set; }
}

public class Comments
{
    public int Id { get; set; }
    public Post Post { get; set; }
    public User Author { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }
}