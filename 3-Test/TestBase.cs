using _2_Blog_CQRS;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace _3_Test;

public abstract class TestBase : IClassFixture<BlogWebAppFactory>
{
    private readonly BlogWebAppFactory _factory;
    //protected readonly IMediator _mediator;
    //protected readonly BlogContext _context;

    protected IMediator GetMediator()
    {
        return _factory.Services.GetRequiredService<IMediator>();
    }

    protected BlogContext GetContext()
    {
        return _factory.Services.GetRequiredService<BlogContext>();
    }

    protected TestBase(BlogWebAppFactory factory)
    {
        _factory = factory;
        factory.CreateClient();
        //_mediator = factory.Services.GetRequiredService<IMediator>();
        //_context = factory.Services.GetRequiredService<BlogContext>();
    }
}