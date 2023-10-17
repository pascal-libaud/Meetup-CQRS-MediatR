using _2_Blog_CQRS;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace _3_Test;

public abstract class TestBase : IClassFixture<BlogWebAppFactory>
{
    private readonly BlogWebAppFactory _factory;
    protected readonly HttpClient _httpClient;

    protected IMediator GetMediator()
    {
        return _factory.Services.GetRequiredService<IMediator>();
    }

    protected BlogContext GetDbContext()
    {
        return _factory.Services.GetRequiredService<BlogContext>();
    }

    protected TestBase(BlogWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }
}