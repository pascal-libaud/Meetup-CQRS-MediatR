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
    public Task<Todo[]> Get()
    {
        return _mediator.Send(new GetAllTodos());
    }

    [HttpGet("{id}")]
    public Task<Todo> GetOne(int id)
    {
        return _mediator.Send(new GetTodo(id));
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