using _0_HelloWorld.Todos;
using _0_HelloWorld.Todos.Command;
using _0_HelloWorld.Todos.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _0_HelloWorld.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<Todo[]> GetAll()
    {
        return _mediator.Send(new GetAllTodos());
    }

    [HttpGet("{id}")]
    public Task<Todo> Get(int id)
    {
        return _mediator.Send(new GetTodo(id));
    }

    [HttpPost]
    public Task Create([FromBody] CreateTodo command)
    {
        return _mediator.Send(command);
    }

    [HttpPatch("{id}")]
    public Task SetIsDone(int id)
    {
        return _mediator.Send(new SetIsDone(id));
    }

    [HttpDelete("{id}")]
    public Task Delete(int id)
    {
        return _mediator.Send(new DeleteTodo(id));
    }
}