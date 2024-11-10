using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.PostView
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/postview/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;

        public PostController(IPostAggregateService postAggregateService)
        {
            _postAggregateService = postAggregateService;
        }

        [HttpGet]
        public ActionResult<List<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _postAggregateService.GetAllPosts(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PostDto> Get(int id)
        {
            var result = _postAggregateService.GetPostById(id);
            return CreateResponse(result);
        }

        [HttpGet("/blogs")]
        public ActionResult<PostDto> GetAggregate(int id)
        {
            var result = _postAggregateService.GetPostById(id);
            return CreateResponse(result);
        }

        [HttpPut("{postId:int}/rate")]
        public ActionResult AddBlogRating(int postId, [FromBody] BlogRatingDto blogRatingDto)
        {
            var result = _postAggregateService.AddRating(postId, blogRatingDto);
            return CreateResponse(result);
        }

        
    }
}
