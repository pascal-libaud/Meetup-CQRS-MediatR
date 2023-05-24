using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace _3_Test;

public abstract class TestBase : IClassFixture<BlogWebAppFactory>
{
    protected readonly IMediator _mediator;

    protected TestBase(BlogWebAppFactory factory)
    {
        factory.CreateClient();
        _mediator = factory.Services.GetRequiredService<IMediator>();
    }
}