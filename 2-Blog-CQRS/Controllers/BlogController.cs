using _2_Blog_CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _2_Blog_CQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<PostDTO[]> GetPosts()
    {
        return _mediator.Send(new GetAllPosts());
    }

    [HttpGet("{id}")]
    public Task<PostDetailDTO> Get(int id)
    {
        return _mediator.Send(new GetDetailedPost(id));
    }

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