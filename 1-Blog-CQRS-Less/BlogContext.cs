using Microsoft.EntityFrameworkCore;

namespace _1_Blog_CQRS_Less;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    { }

    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Comments> Comments { get; set; }
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
        };

        var post2 = new Post
        {
            Author = nicolas,
            Title = "La Clean Architecture en 3 leçons",
            Content = "Uncle Bob..."
        };

        var post3 = new Post
        {
            Author = thomas,
            Title = "Le TDD pour les nuls",
            Content = "Red Green Refactor and co"
        };

        post1.Comments.Add(new Comments
        {
            Author = thomas,
            Content = "Très intéressant"
        });

        post2.Comments.Add(new Comments
        {
            Author = pascal,
            Content = "C'est passionnant"
        });

        post2.Comments.Add(new Comments
        {
            Author = thomas,
            Content = "Intructif"
        });

        context.Users.AddRange(pascal, nicolas, thomas);
        context.Posts.AddRange(post1, post2, post3);

        context.SaveChanges();
    }
}