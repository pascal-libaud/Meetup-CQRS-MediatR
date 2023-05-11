using _2_Blog_CQRS.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // MediatR configuration
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<Program>();
            configuration.Lifetime = ServiceLifetime.Transient;
        })
            // TODO: Quel type de service pour les pipelines (Scoped, Singleton, Transient) ?
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformancePipeline<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(RetryPipeline<,>));

        // SQLite InMemory configuration
        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();

        builder.Services.AddDbContext<BlogContext>(options =>
        {
            options.UseSqlite(keepAliveConnection);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        // FluentValidation configuration
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseCustomExceptionHandler();

        // Seed the Database
        var blogContext = app.Services.GetService<BlogContext>()!;
        blogContext.Database.Migrate();
        Seeder.Seed(blogContext);

        app.Run();
    }
}