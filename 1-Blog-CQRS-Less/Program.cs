using _1_Blog_CQRS_Less.Helpers;
using _1_Blog_CQRS_Less.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace _1_Blog_CQRS_Less;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<PerfHelper>();
        builder.Services.AddTransient<IPostService, PostService>();
        builder.Services.AddTransient<IUserService, UserService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddValidatorsFromAssemblyContaining<CreatePostValidator>();
        builder.Services.AddFluentValidationAutoValidation(configuration =>
{
            // Disable the built-in .NET model (data annotations) validation.
            configuration.DisableBuiltInModelValidation = true;

            // Only validate controllers decorated with the `FluentValidationAutoValidation` attribute.
            configuration.ValidationStrategy = ValidationStrategy.All;

            // Replace the default result factory with a custom implementation.
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });
        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder => builder.Tag("Post"));
        });

        // SQLite InMemory configuration
        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();

        builder.Services.AddDbContext<BlogContext>(options =>
        {
            options.UseSqlite(keepAliveConnection);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseOutputCache();
        app.MapControllers();
        app.UseCustomExceptionHandler();

        // Seed the Database
        var blogContext = app.Services.GetService<BlogContext>()!;
        blogContext.Database.Migrate();
        Seeder.Seed(blogContext);

        app.Run();
    }
}

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new BadRequestObjectResult(new { Title = "Validation errors", ValidationErrors = validationProblemDetails?.Errors });
    }
}