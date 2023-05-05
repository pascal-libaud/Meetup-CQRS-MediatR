using _2_Blog_CQRS.Commands.Posts;
using _2_Blog_CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _2_Blog_CQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
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

    [HttpPost]
    public Task PostPost([FromBody] CreatePost createPost)
    {
        return _mediator.Send(createPost);
    }

    [HttpDelete]
    public Task Delete(int id)
    {
        return _mediator.Send(new DeletePost(id));
    }
}