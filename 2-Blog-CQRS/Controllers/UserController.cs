using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _2_Blog_CQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpPost]
    //public Task CreateUser()
    //{

    //}

    //[HttpPatch("{id}")]
    //public Task SetIsDone(int id)
    //{
    //    return _mediator.Send(new SetIsDone(id));
    //}

    //[HttpDelete("{id}")]
    //public Task Delete(int id)
    //{
    //    return _mediator.Send(new DeleteTodo(id));
    //}
}