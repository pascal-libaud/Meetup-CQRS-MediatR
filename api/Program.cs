using Api;
using Core.Todos;
using Core.Todos.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Todo).Assembly);

    configuration.NotificationPublisher = new MyNotificationPublisher();
    //configuration.AddBehavior<>()
});

builder.Services.AddTransient<Repository>();

var app = builder.Build();

var repository = app.Services.GetRequiredService<Repository>();
await TodoSeeder.Seed(repository);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();