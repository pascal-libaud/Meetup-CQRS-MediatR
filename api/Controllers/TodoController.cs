using Core.Todos;
using Core.Todos.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private readonly ILogger<TodoController> _logger;
    private readonly IMediator _mediator;

    public TodoController(ILogger<TodoController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public Task<Todo[]> Get()
    {
        return _mediator.Send(new GetAllTodos());
    }

    [HttpGet("{id}")]
    public Task<Todo> GetOne(Guid id)
    {
        return _mediator.Send(new GetTodo(id));
    }
}