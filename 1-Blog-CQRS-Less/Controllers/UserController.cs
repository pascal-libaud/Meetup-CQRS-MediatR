using _1_Blog_CQRS_Less.Helpers;
using _1_Blog_CQRS_Less.Services;
using Microsoft.AspNetCore.Mvc;

namespace _1_Blog_CQRS_Less.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly PerfHelper _perfHelper;

    public UserController(UserService userService, ILogger<UserController> logger, PerfHelper perfHelper)
    {
        _userService = userService;
        _logger = logger;
        _perfHelper = perfHelper;
    }

    [HttpGet]
    public async Task<UserDTO[]> GetUsers(CancellationToken cancellationToken)
    {
        return await _userService.GetUsers(cancellationToken);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDTO> GetUser(int id, CancellationToken cancellationToken)
    {
        return await _userService.GetUser(id, cancellationToken);
    }

    [HttpPost]
    public async Task<int> CreateUser([FromBody] UserDTO createUser, CancellationToken cancellationToken)
    {
        return await _perfHelper.MeasurePerformances(async () =>
        {
            return await _userService.CreateUser(createUser, cancellationToken);
        });
    }

    [HttpPatch("{id}")]
    public async Task RenameUser(int id, [FromBody] string name, CancellationToken cancellationToken)
    {
        await _userService.RenameUser(id, name, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUser(id, cancellationToken);
    }
}
