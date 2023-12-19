using _1_Blog_CQRS_Less.Helpers;
using _1_Blog_CQRS_Less.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace _1_Blog_CQRS_Less.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly PerfHelper _perfHelper;
        private readonly ILogger<PostController> _logger;

        public PostController(PostService postService, PerfHelper perfHelper, ILogger<PostController> logger)
        {
            _postService = postService;
            _perfHelper = perfHelper;
            _logger = logger;
        }

        [HttpGet]
        [OutputCache]
        public async Task<PostDTO[]> GetPosts(CancellationToken cancellationToken)
        {
            return await _perfHelper.MeasurePerformances(async () =>
            {
                return await _postService.GetPosts(cancellationToken);
            });
        }

        [HttpGet("{id}")]
        public async Task<PostDTO> Get(int id, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            return await _postService.GetPost(id, cancellationToken);
        }

        [HttpPost]
        public async Task PostPost([FromBody] PostDTO postDTO, IOutputCacheStore cacheStorage, CancellationToken cancellationToken)
        {
            await _postService.CreatePost(postDTO, cancellationToken);
            await cacheStorage.EvictByTagAsync("Post", cancellationToken);
        }

        [HttpDelete]
        public Task Delete(int id, CancellationToken cancellationToken)
        {
            return _postService.DeletePost(id, cancellationToken);
        }
    }
}
