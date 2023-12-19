using System.Net;
using System.Net.Http.Json;
using _2_Blog_CQRS.Domain.Posts.Commands;

namespace _3_Test;

public class PostTestUsingHttpClient : TestBase
{
    const string LoremIpsum = """
                              Lorem ipsum dolor sit amet, consectetuer adipiscing elit.
                              Maecenas porttitor congue massa.
                              Fusce posuere, magna sed pulvinar ultricies, ...
                              """;

    public PostTestUsingHttpClient(BlogWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task When_I_try_to_create_a_post_with_a_too_short_title_then_I_got_an_error()
    {
        var createPost = new CreatePost("Ada", LoremIpsum, "Pascal");

        var content = JsonContent.Create(createPost);
        var response = await _httpClient.PostAsync("Post", content, CancellationToken.None);

        var responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        responseContent.Should()
            .Be("Validation failed: \r\n -- Title: 'Title' doit être supérieur ou égal à 5 caractères. Vous avez saisi 3 caractères. Severity: Error");
    }

    [Fact]
    public async Task When_I_try_to_update_a_post_with_a_too_short_title_then_I_got_an_error()
    {
        var updatePost = new UpdatePost(1, "Ada", LoremIpsum);

        var content = JsonContent.Create(updatePost);
        var response = await _httpClient.PatchAsync($"Post/{updatePost.Id}", content, CancellationToken.None);

        var responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        responseContent.Should()
            .Be("Validation failed: \r\n -- Title: 'Title' doit être supérieur ou égal à 5 caractères. Vous avez saisi 3 caractères. Severity: Error");
    }
}