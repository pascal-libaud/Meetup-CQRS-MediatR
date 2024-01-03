using _2_Blog_CQRS.Domain;
using _2_Blog_CQRS.Pipelines;
using FluentValidation;
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
        builder.Services.AddMemoryCache();

        // MediatR configuration
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<Program>();
            configuration.Lifetime = ServiceLifetime.Transient;

            configuration.NotificationPublisherType = typeof(MyNotificationPublisher);

            configuration.AddOpenBehavior(typeof(PerformancePipeline<,>));
            configuration.AddOpenBehavior(typeof(MemoryCachePipeline<,>));
            configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
            configuration.AddOpenBehavior(typeof(RetryPipeline<,>));
            configuration.AddOpenBehavior(typeof(TransactionPipeline<,>));
        });

        // SQLite InMemory configuration
        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();

        builder.Services.AddDbContext<BlogContext>(options =>
        {
            options.UseSqlite(keepAliveConnection);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        // FluentValidation configuration
        builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Transient);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseCustomExceptionHandler();

        app.MapControllers();

        // Seed the Database
        var blogContext = app.Services.GetService<BlogContext>()!;
        blogContext.Database.Migrate();
        Seeder.Seed(blogContext);

        app.Run();
    }
}