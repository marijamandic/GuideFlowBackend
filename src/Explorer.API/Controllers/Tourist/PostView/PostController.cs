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
        private readonly IPostService _postService;
        public PostController(IPostService postService, IPostAggregateService postAggregate)
        {
            _postService = postService;
            _postAggregateService = postAggregate;
        }

        [HttpGet]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<PostDto>> Get(int id)
        {
            var result = _postService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("/blogs")]
        public ActionResult<PagedResult<PostDto>> GetAggregate(int id)
        {
            var result = _postAggregateService.GetPostById(id);
            return CreateResponse(result);
        }

        [HttpPut("{invitationId:int}/accept")]
        public ActionResult<PagedResult<PostDto>> AddBlogRating(int invitationId, [FromBody] BlogRatingDto blogRatingDto)
        {
            var result = _postAggregateService.GetPostById(invitationId);
            var post = result.Value;
            
            if (post == null)
            {
                return BadRequest("There is no such a post.");
            }

            var newRatingResult = _postAggregateService.AddRating(post.Id, blogRatingDto);

            return CreateResponse(newRatingResult);
        }

    }
}
