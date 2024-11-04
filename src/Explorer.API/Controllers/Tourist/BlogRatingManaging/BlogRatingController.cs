using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.BlogRatingManaging
{
    [Route("api/blogRatingManaging/blogRating")]
    public class BlogRatingController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;
        private readonly IUserService _userService;

        public BlogRatingController(IPostAggregateService postAggregateService, IUserService userService)
        {
            _postAggregateService = postAggregateService;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] BlogRatingDto blogRating)
        {
            var result = _postAggregateService.AddRating(blogRating.PostId, blogRating);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<BlogRatingDto>> GetAllRatingsForPost([FromQuery] long postId)
        {
            var result = _postAggregateService.GetRatingsForPost(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] long userId, [FromQuery] long postId)
        {
            var result = _postAggregateService.DeleteRating(userId, postId);
            return CreateResponse(result);
        }
    }
}
