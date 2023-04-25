using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    { }

    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Comment> Comments { get; set; }
}

public static class Seeder
{
    public static void Seed(BlogContext context)
    {
        var pascal = new User { Name = "Pascal" };
        var nicolas = new User { Name = "Nicolas" };
        var thomas = new User { Name = "Thomas" };

        var post1 = new Post
        {
            Author = pascal,
            Title = "Quelles sont les nouveautés de .net 7 ?",
            Content = "blablabla",
            Comments =
            {
                new Comment { Author = thomas, Content = "Très intéressant" }
            }
        };

        var post2 = new Post
        {
            Author = nicolas,
            Title = "La Clean Architecture en 3 leçons",
            Content = "Uncle Bob...",
            Comments =
            {
                new Comment { Author = pascal, Content = "C'est passionnant" },
                new Comment { Author = thomas, Content = "Intructif" }
            }
        };

        var post3 = new Post
        {
            Author = thomas,
            Title = "Le TDD pour les nuls",
            Content = "Red Green Refactor and co"
        };

        context.Users.AddRange(pascal, nicolas, thomas);
        context.Posts.AddRange(post1, post2, post3);

        context.SaveChanges();
    }
}