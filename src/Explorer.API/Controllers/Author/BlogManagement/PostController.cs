using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.BlogManagement
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/blogManagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery]int page, [FromQuery]int pageSize) {
            var result = _postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PostDto> Create([FromBody] PostDto post) {
            var result = _postService.Create(post);
            return CreateResponse(result);
        }
    }
}
