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
        public ActionResult<BlogRatingDto> Create([FromBody] BlogRatingDto blogRating)
        {
            var result = _postAggregateService.AddRating(blogRating.PostId, blogRating);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Message);
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
            if (result.IsSuccess)
            {
                return Ok(); // Return 200 OK on successful deletion
            }

            // Check if the error is "Comment not found" and return 404 if so
            if (result.Errors.Any(e => e.Message == "Comment not found"))
            {
                return NotFound("Comment not found."); // Return 404 Not Found for missing comments
            }

            // Log other errors and return 500 for unexpected issues
            Console.WriteLine($"Delete failed with error: {result.Errors.FirstOrDefault()?.Message}");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
