using _2_Blog_CQRS;
using _2_Blog_CQRS.Commands.Posts;
using _2_Blog_CQRS.Queries.Posts;
using FluentValidation;

namespace _3_Test;

public class PostTest : TestBase
{
    const string LoremIpsum = """
                         Lorem ipsum dolor sit amet, consectetuer adipiscing elit.
                         Maecenas porttitor congue massa.
                         Fusce posuere, magna sed pulvinar ultricies, ...
                         """;

    public PostTest(BlogWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task By_default_I_should_get_3_posts_in_the_database()
    {
        var posts = await GetMediator().Send(new GetAllPosts());
        posts.Length.Should().Be(3);
    }

    [Fact]
    public async Task When_I_try_to_create_a_post_with_a_too_short_title_then_I_got_an_error()
    {
        var createPost = () => GetMediator().Send(new CreatePost("Ada", LoremIpsum, "Pascal"));

        var exception = (await createPost.Should().ThrowAsync<ValidationException>()).And;
        var errors = exception.Errors.ToList();

        errors.Count.Should().Be(1);
        errors[0].PropertyName.Should().Be(nameof(Post.Title));
    }

    [Fact]
    public async Task When_I_try_to_update_a_post_with_a_too_short_title_then_I_got_an_error()
    {
        var createPost = () => GetMediator().Send(new UpdatePost(1, "Ada", LoremIpsum));

        var exception = (await createPost.Should().ThrowAsync<ValidationException>()).And;
        var errors = exception.Errors.ToList();

        errors.Count.Should().Be(1);
        errors[0].PropertyName.Should().Be(nameof(Post.Title));
    }
}