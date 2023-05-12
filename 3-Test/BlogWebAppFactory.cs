using _2_Blog_CQRS;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace _3_Test;

public class BlogWebAppFactory : WebApplicationFactory<Program>
{
    public IMediator Mediator { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            Mediator = serviceProvider.GetService<IMediator>()!;
        });
    }
}