using _2_Blog_CQRS.Domain.Users.Commands;
using _2_Blog_CQRS.Domain.Users.Queries;
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

    [HttpGet]
    [Route("{allUsers}")]
    public Task<UserDTO[]> GetUsers(bool allUsers)
    {
        return _mediator.Send<UserDTO[]>(allUsers ? new GetAllUsers() : new GetActiveUsers());
    }

    [HttpGet]
    [Route("{id}")]
    public Task<UserDetailDTO> GetUser(int id)
    {
        return _mediator.Send(new GetDetailedUser(id));
    }

    [HttpPost]
    public Task<int> CreateUser([FromBody] CreateUser createUser)
    {
        return _mediator.Send(createUser);
    }

    [HttpPatch("{id}")]
    public Task RenameUser(int id, [FromBody] PatchUser renameUser)
    {
        return _mediator.Send(new RenameUser(id, renameUser.Name));
    }

    [HttpDelete("{id}")]
    public Task DeleteUser(int id)
    {
        return _mediator.Send(new DeleteUser(id));
    }
}

public record PatchUser(string Name);