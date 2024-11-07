using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.CommentManaging
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/commentmanaging/comment")]
    public class CommentController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;
        private readonly IUserService _userService;

        public CommentController(IPostAggregateService postAggregateService, IUserService userService)
        {
            _postAggregateService = postAggregateService;
            _userService = userService;
        }

        [HttpGet("all")]
        public ActionResult<List<CommentDto>> GetAllCommentsForPost([FromQuery] long postId)
        {
            var result = _postAggregateService.GetCommentsForPost(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

        [HttpGet("user/{id:int}")]
        public ActionResult<UserDto> GetCommentCreator(int id)
        {
            var result = _userService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CommentDto> Create([FromBody] CommentDto comment)
        {
            var result = _postAggregateService.AddComment(comment.PostId, comment);
            if (result.IsSuccess)
            {
                return Ok(result.Value); 
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommentDto> Update([FromBody] CommentDto comment, int id)
        {
            comment.Id = id;  
            var result = _postAggregateService.UpdateComment(comment.PostId, comment);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Returns the updated CommentDto
            }

            // Log the error message for easier troubleshooting
            Console.WriteLine($"Update failed with error: {result.Errors.FirstOrDefault()?.Message}");
            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _postAggregateService.DeleteComment(id);
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

        [HttpGet("count")]
        public ActionResult<int> GetCommentCount([FromQuery] int postId)
        {
            var result = _postAggregateService.GetCommentCountForPost(postId);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

    }
}
