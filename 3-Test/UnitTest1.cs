using _2_Blog_CQRS.Queries.Posts;

namespace _3_Test;

public class UnitTest1 : TestBase
{
    public UnitTest1(BlogWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Test1()
    {
        var posts = await _mediator.Send(new GetAllPosts());

        posts.Length.Should().Be(3);
    }
}