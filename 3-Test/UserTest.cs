using _2_Blog_CQRS;
using _2_Blog_CQRS.Commands.Users;
using Microsoft.EntityFrameworkCore;

namespace _3_Test;

public class UserTest : TestBase
{
    public UserTest(BlogWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task When_a_user_is_deleted_their_comments_are_also_deleted()
    {
        // ARRANGE
        var musk = new User { IsDeleted = false, Name = "Elon Musk" };
        var dev = new User { IsDeleted = false, Name = "Lambda developper" };

        var post = new Post
        {
            Author = musk,
            Title = "I'm the king",
            Content = "Twitter is better than ever",
            Comments =
            {
                new Comment { Author = dev, Content = "You should listen to your engineers" }
            }
        };

        var context = GetDbContext();

        await context.Users.AddRangeAsync(musk, dev);
        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();

        // ACT
        await GetMediator().Send(new DeleteUser(dev.Id));

        // ASSERT
        context = GetDbContext();
        var devComments = await context.Comments.Where(c => c.Author.Id == dev.Id).ToListAsync();
        devComments.Should().NotBeNullOrEmpty();
        devComments.Should().AllSatisfy(c => c.IsDeleted.Should().BeTrue());
    }
}