using _2_Blog_CQRS.Commands.Users;
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

    [HttpPost]
    public Task<int> CreateUser([FromBody] CreateUser createUser)
    {
        return _mediator.Send(createUser);
    }

    [HttpPatch("{id}")]
    public Task RenameUser(int id, [FromBody] RenameUser renameUser)
    {
        return _mediator.Send(renameUser with { Id = id }); // TODO Discuter de ce code ?!
    }

    [HttpDelete("{id}")]
    public Task DeleteUser(int id)
    {
        return _mediator.Send(new DeleteUser(id));
    }
}