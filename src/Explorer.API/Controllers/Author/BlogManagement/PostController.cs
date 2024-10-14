using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.BlogManagement
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/blogManagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) {
            _postService = postService;
        }

        [HttpPost]
        public ActionResult<PostDto> Create([FromBody] PostDto post) {
            var result = _postService.Create(post);
            return CreateResponse(result);
        }
    }
}
